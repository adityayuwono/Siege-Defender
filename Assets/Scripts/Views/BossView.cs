using System.Collections;
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
        private readonly EnemyManagerView _parent;
        public BossView(Boss viewModel, EnemyManagerView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;
        }

        protected GameObject CharacterRoot
        {
            get
            {
                if (_character == null)
                {
                    var characterRoot = Transform.FindChild(Values.Defaults.BOSS_CHARACTER_ROOT_TAG);
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

            _viewModel.MoveToARandomWaypoint.OnChange += MoveToARandomWaypoint;

            for (var i = 0; i < Transform.childCount; i++)
            {
                var child = Transform.GetChild(i);
                if (child.name.Contains(Values.Defaults.WAYPOINT_TRANSFORM_TAG))
                    _waypoints.Add(child);
            }

            var firstWaypoint = _waypoints[0];
            _waypoints.Remove(firstWaypoint);
            _waypoints.Add(firstWaypoint);

            _viewModel.MoveToARandomWaypoint.SetValue(false);
        }

        protected override void OnDestroy()
        {
            _viewModel.MoveToARandomWaypoint.OnChange -= MoveToARandomWaypoint;

            _waypoints.Clear();

            base.OnDestroy();
        }

        protected override Animator GetAnimator()
        {
            return CharacterRoot.GetComponent<Animator>();
        }

        private void MoveToARandomWaypoint()
        {
            if (_viewModel.MoveToARandomWaypoint.GetValue())
            {
                var randomWaypointIndex = _randomizer.Next(_waypoints.Count-1);// Avoid randomizing the last on the list
                var randomWaypoint = _waypoints[randomWaypointIndex];
                // Funky algo to let the used waypoint to be last on the list, to avoid going to the same waypoint, it looks stupid, seriously
                _waypoints.Remove(randomWaypoint);
                _waypoints.Add(randomWaypoint);
                // Ok, start moving
                _viewModel.Root.StartCoroutine(MoveToWaypoint(randomWaypoint));
            }
        }

        private const string EASE_TYPE = "linear";
        private IEnumerator MoveToWaypoint(Transform waypoint)
        {
            var walkDuration = Vector3.Distance(waypoint.localPosition, _characterTransform.localPosition) / _viewModel.BossSpeed;
            var lookToDuration = Quaternion.Angle(waypoint.rotation, _characterTransform.rotation) / 24f / _viewModel.BossSpeed;

            // Initiate walking sequence
            Animate_SetBool(Values.Defaults.WALKING_ANIMATION_BOOL_TAG, true);

            // Turn to face the destination
            iTween.LookTo(CharacterRoot, iTween.Hash("looktarget", waypoint, "easetype", EASE_TYPE, "time", lookToDuration));
            yield return new WaitForSeconds(lookToDuration);
            
            // Walk to reach the destination
            iTween.MoveTo(CharacterRoot, iTween.Hash("position", waypoint, "easetype", EASE_TYPE, "time", walkDuration));
            yield return new WaitForSeconds(walkDuration);

            // Turn to match the destination's rotation
            var rotateDuration = Quaternion.Angle(waypoint.localRotation, _characterTransform.localRotation)/24f/_viewModel.BossSpeed;
            iTween.RotateTo(CharacterRoot, iTween.Hash("rotation", waypoint, "easetype", EASE_TYPE, "time", rotateDuration));
            yield return new WaitForSeconds(rotateDuration);

            // Finished walking sequence
            Animate_SetBool(Values.Defaults.WALKING_ANIMATION_BOOL_TAG, false);
            _viewModel.MoveToARandomWaypoint.SetValue(false);
        }

        protected override void SetPosition()
        {
            // The boss always spawns in the middle, for now...
            var randomPosition = _parent.GetRandomSpawnPoint();
            randomPosition.x = 0;
            randomPosition.z = 0;
            Transform.localPosition = randomPosition;
        }
    }
}
