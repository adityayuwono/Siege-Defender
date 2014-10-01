using System.Collections;
using System.Collections.Generic;
using Scripts.Components;
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

        private GameObject _character;
        private Transform _characterTransform;
        
        private readonly System.Random _randomizer = new System.Random();
        private readonly List<Transform> _waypoints = new List<Transform>();

        protected override void OnLoad()
        {
            base.OnLoad();

            var characterRoot = Transform.FindChild("Character");
            if (characterRoot != null)
            {
                _characterTransform = characterRoot;
                _character = characterRoot.gameObject;
                _character.AddComponent<ViewModelController>().ViewModel = _viewModel;
            }

            _viewModel.MoveToARandomWaypoint.OnChange += MoveToARandomWaypoint;

            for (var i = 0; i < Transform.childCount; i++)
            {
                var child = Transform.GetChild(i);
                if (child.name.Contains("Waypoint"))
                {
                    _waypoints.Add(child);
                }
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
            return Transform.FindChild("Character").GetComponent<Animator>();
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

        private IEnumerator MoveToWaypoint(Transform waypoint)
        {
            // Initiate walking sequence
            Animate_SetBool("IsWalking", true);

            // Turn to face the destination
            var walkDuration = Vector3.Distance(waypoint.localPosition, _characterTransform.localPosition)/_viewModel.BossSpeed;
            var lookToDuration = Vector3.Angle(waypoint.localEulerAngles, _characterTransform.localEulerAngles)/24f/_viewModel.BossSpeed;
            iTween.LookTo(_character, iTween.Hash("looktarget", waypoint, "easetype", "linear", "time", lookToDuration));
            yield return new WaitForSeconds(lookToDuration*0.75f);
            
            // Walk to reach the destination
            iTween.MoveTo(_character, iTween.Hash("position", waypoint, "easetype", "linear", "time", walkDuration));
            yield return new WaitForSeconds(walkDuration);

            // Turn to match the destination's rotation
            var rotateDuration = Quaternion.Angle(waypoint.localRotation, _characterTransform.localRotation)/24f/_viewModel.BossSpeed;
            iTween.RotateTo(_character, iTween.Hash("rotation", waypoint, "easetype", "linear", "time", rotateDuration));
            yield return new WaitForSeconds(rotateDuration);

            // Finished walking sequence
            Animate_SetBool("IsWalking", false);
            _viewModel.MoveToARandomWaypoint.SetValue(false);
        }

        protected override void SetPosition()
        {
            // The boss always spawn in the middle, for now...
            var randomPosition = _parent.GetRandomSpawnPoint();
            randomPosition.x = 0;
            randomPosition.z = 0;
            Transform.localPosition = randomPosition;
        }
    }
}
