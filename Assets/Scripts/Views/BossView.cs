using System;
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
            _waypoints.Clear();

            base.OnDestroy();
        }

        protected override Animator GetAnimator()
        {
            return CharacterRoot.GetComponent<Animator>();
        }

        #region Move to Waypoint
        private void MoveToARandomWaypoint()
        {
            _isMovementInterrupted = false;
            var randomWaypointIndex = _randomizer.Next(_waypoints.Count-1);// Avoid randomizing the last on the list
            var randomWaypoint = _waypoints[randomWaypointIndex];
            // Funky algo to let the used waypoint to be last on the list, to avoid going to the same waypoint, it looks stupid, seriously
            _waypoints.Remove(randomWaypoint);
            _waypoints.Add(randomWaypoint);
            // Ok, start moving
            _viewModel.Root.StartCoroutine(LookToWayPoint(randomWaypoint));
        }
        private bool _isMovementInterrupted;
        private void InterruptMovement()
        {
            // TODO: Try Interval runner feature, may be cleaner
            _isMovementInterrupted = true;
            iTween.Stop(CharacterRoot);
            _onMoveContinue = null;
            FinishedWalking();
        }

        private const string EASE_TYPE = "linear";
        private Action _onMoveContinue;

        private IEnumerator LookToWayPoint(Transform waypoint)
        {
            // Initiate walking sequence
            _viewModel.AnimationId.SetValue(Values.Defaults.WALKING_ANIMATION_BOOL_TAG);

            // Turn to face the destination
            var lookToDuration = Quaternion.Angle(waypoint.rotation, _characterTransform.rotation) / 24f / _viewModel.BossSpeed;
            if (!_isMovementInterrupted)
            {
                iTween.LookTo(CharacterRoot, iTween.Hash("looktarget", waypoint, "easetype", EASE_TYPE, "time", lookToDuration));
                _onMoveContinue += () =>
                {
                    _onMoveContinue = null;
                    _viewModel.Root.StartCoroutine(MoveToWaypoint(waypoint));
                };

                yield return new WaitForSeconds(lookToDuration);
                if (_onMoveContinue != null)
                    _onMoveContinue();
            }
        }
        private IEnumerator MoveToWaypoint(Transform waypoint)
        {
            var walkDuration = Vector3.Distance(waypoint.localPosition, _characterTransform.localPosition)/_viewModel.BossSpeed;
            // Walk to reach the destination
            if (!_isMovementInterrupted)
            {
                iTween.MoveTo(CharacterRoot, iTween.Hash("position", waypoint, "easetype", EASE_TYPE, "time", walkDuration));
                _onMoveContinue += () =>
                {
                    _onMoveContinue = null;
                    _viewModel.Root.StartCoroutine(LookFromWaypoint(waypoint));
                };
                yield return new WaitForSeconds(walkDuration);
                if (_onMoveContinue != null)
                    _onMoveContinue();
            }
        }
        private IEnumerator LookFromWaypoint(Transform waypoint)
        {
            // Turn to match the destination's rotation
            var rotateDuration = Quaternion.Angle(waypoint.localRotation, _characterTransform.localRotation)/24f/_viewModel.BossSpeed;
            if (!_isMovementInterrupted)
            {
                iTween.RotateTo(CharacterRoot, iTween.Hash("rotation", waypoint, "easetype", EASE_TYPE, "time", rotateDuration));
                _onMoveContinue += () =>
                {
                    _onMoveContinue = null;
                    FinishedWalking();
                };
                yield return new WaitForSeconds(rotateDuration);
                if (_onMoveContinue != null)
                    _onMoveContinue();
            }
        }
        private void FinishedWalking()
        {
            // Finished walking sequence
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
