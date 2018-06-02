using System.Collections.Generic;
using Scripts.Components;
using Scripts.Helpers;
using Scripts.ViewModels.Enemies;
using UnityEngine;

namespace Scripts.Views
{
    public class BossView : EnemyBaseView
    {
        private readonly Boss _viewModel;
        public BossView(Boss viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected GameObject CharacterRoot
        {
            get
            {
                if (_character == null)
                {
                    var characterRoot = Transform.Find(Values.Defaults.BOSS_CHARACTER_ROOT_TAG);
                    if (characterRoot != null)
                    {
                        _characterTransform = characterRoot;
                        _character = characterRoot.gameObject;
                        _character.AddComponent<ViewModelController>().ViewModel = _viewModel;
                    }
                }
                return _character;
            }
        }
        private GameObject _character;
        private Transform _characterTransform;
        
        private readonly System.Random _randomizer = new System.Random();
        private readonly List<Transform> _waypoints = new List<Transform>();

        protected override void OnLoad()
        {
            base.OnLoad();

            _viewModel.OnInterrupted += InterruptMovement;
            _viewModel.OnMoveStart += MoveToARandomWaypoint;

            for (var i = 0; i < Transform.childCount; i++)
            {
                var child = Transform.GetChild(i);
                if (child.name.Contains(Values.Defaults.WAYPOINT_TRANSFORM_TAG))
                    _waypoints.Add(child);
            }

            var firstWaypoint = _waypoints[0];
            _waypoints.Remove(firstWaypoint);
            _waypoints.Add(firstWaypoint);
        }

        protected override void OnDestroy()
        {
            _viewModel.OnInterrupted -= InterruptMovement;
            _viewModel.OnMoveStart -= MoveToARandomWaypoint;
            
            _waypoints.Clear();
            
            InterruptMovement();// Cancel current movements, just to be safe

            base.OnDestroy();
        }

        protected override Animator GetAnimator()
        {
            return CharacterRoot.GetComponent<Animator>();
        }

        #region Move to Waypoint

        private Transform GetRandomWaypoint()
        {
            if (_waypoints.Count == 0)
                throw new EngineException(this, "No Waypoints defined");

            var randomWaypointIndex = _randomizer.Next(_waypoints.Count - 1);// Avoid randomizing the last on the list
            var randomWaypoint = _waypoints[randomWaypointIndex];

            // Funky algo to let the used waypoint to be last on the list, to avoid going to the same waypoint, it looks stupid, seriously
            _waypoints.Remove(randomWaypoint);
            _waypoints.Add(randomWaypoint);

            return randomWaypoint;
        }

        private void MoveToARandomWaypoint(ViewModels.Object targetObject, float speedMultiplier)
        {
            // Multiply the speed, in case the normal walk and this Walk "Skill" is different
            var speed = _viewModel.BossSpeed*speedMultiplier;

            Transform moveTargetPosition;
	        if (targetObject == null)
	        {
		        moveTargetPosition = GetRandomWaypoint();
	        }
	        else
	        {
		        moveTargetPosition = _viewModel.Root.GetView<ObjectView>(targetObject).Transform;
	        }

            var targetLookatRotation = Quaternion.LookRotation(moveTargetPosition.position - _characterTransform.position);

            // Register everything here
            // Starting with the very first motion in the pattern
            var lookToAngle = Quaternion.Angle(targetLookatRotation, _characterTransform.rotation);
            var lookToDuration = lookToAngle / 24f / speed;
            
            const string easeType = "linear";
            iTween.LookTo(CharacterRoot, iTween.Hash("looktarget", moveTargetPosition, "easetype", easeType, "time", lookToDuration));
            var walkDuration = Vector3.Distance(moveTargetPosition.localPosition, _characterTransform.localPosition) / speed;
            iTween.MoveTo(CharacterRoot, iTween.Hash("position", moveTargetPosition, "easetype", easeType, "time", walkDuration, "delay", lookToDuration));

            var rotateDuration = 0f;
            if (targetObject == null)
            {
                rotateDuration = (lookToAngle + Quaternion.Angle(moveTargetPosition.localRotation, _characterTransform.localRotation))/24f/speed;
                iTween.RotateTo(CharacterRoot, iTween.Hash("rotation", moveTargetPosition, "easetype", easeType, "time", rotateDuration, "delay", walkDuration + lookToDuration));
            }
            
            _viewModel.Root.IntervalRunner.SubscribeToInterval(FinishedWalking, walkDuration + lookToDuration + rotateDuration, false);
        }
        private void InterruptMovement()
        {
            FinishedWalking();
            iTween.Stop(CharacterRoot);
        }

        private void FinishedWalking()
        {
            // Finished walking sequence
            _viewModel.Root.IntervalRunner.UnsubscribeFromInterval(FinishedWalking);
            _viewModel.AnimationId.SetValue("Idle");
            _viewModel.FinishedMovement();
        }
        #endregion

        protected override void SetPosition()
        {
            // The boss always spawns in the middle, for now...
            base.SetPosition();
            var randomPosition = Transform.localPosition;
            randomPosition.x = 0;
            randomPosition.y = 0;
            Transform.localPosition = randomPosition;
        }
    }
}
