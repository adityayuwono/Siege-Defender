using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.ViewModels.Enemies;
using UnityEngine;
using Object = Scripts.ViewModels.Object;
using Random = System.Random;

namespace Scripts.Views.Enemies
{
	public class BossView : EnemyView
	{
		private readonly Random _randomizer = new Random();
		private readonly Boss _viewModel;
		private readonly List<Transform> _waypoints = new List<Transform>();

		public BossView(Boss viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_viewModel.OnInterrupted += InterruptMovement;
			_viewModel.OnMoveStart += MoveToRandomWaypoint;

			for (var i = 0; i < Transform.childCount; i++)
			{
				var child = Transform.GetChild(i);
				if (child.name.Contains(Values.Defaults.WaypointTransformTag))
					_waypoints.Add(child);
			}

			var firstWaypoint = _waypoints[0];
			_waypoints.Remove(firstWaypoint);
			_waypoints.Add(firstWaypoint);
		}

		protected override void LoadTarget()
		{
			// No target for Boss, they don't move towards the player
		}

		protected override void AdjustRotation()
		{
			var randomRotation = 180f + UnityEngine.Random.value * UnityEngine.Random.Range(-1, 2) * _viewModel.Rotation;
			Transform.localEulerAngles = new Vector3(0, randomRotation, 0);
		}

		protected override void OnDestroy()
		{
			_viewModel.OnInterrupted -= InterruptMovement;
			_viewModel.OnMoveStart -= MoveToRandomWaypoint;

			_waypoints.Clear();

			InterruptMovement(); // Cancel current movements, just to be safe

			base.OnDestroy();
		}

		protected override void StartWalking()
		{
			Animator.SetBool("Death", false);
			_viewModel.Root.Context.IntervalRunner.SubscribeToInterval(Walk);
		}

		protected override void SetPosition()
		{
			// The boss always spawns in the middle, for now...
			base.SetPosition();
			var randomPosition = Transform.localPosition;
			randomPosition.x = 0;
			randomPosition.y = 0;
			Transform.localPosition = randomPosition;
		}

		#region Move to Waypoint

		private Transform GetRandomWaypoint()
		{
			if (_waypoints.Count == 0) throw new EngineException(this, "No Waypoints defined");

			var waypointIndex = _randomizer.Next(_waypoints.Count - 1); // Avoid randomizing the last on the list
			var waypoint = _waypoints[waypointIndex];

			// Funky algo to let the used waypoint to be last on the list, to avoid going to the same waypoint, it looks stupid, seriously
			_waypoints.Remove(waypoint);
			_waypoints.Add(waypoint);

			return waypoint;
		}

		private void MoveToRandomWaypoint(Object targetObject, float speedMultiplier)
		{
			// Multiply the speed, in case the normal walk and this Walk "Skill" is different
			var speed = _viewModel.BossSpeed * speedMultiplier;

			Transform moveTargetPosition;
			if (targetObject == null)
			{
				moveTargetPosition = GetRandomWaypoint();
			}
			else
			{
				moveTargetPosition = _viewModel.Root.GetView<ObjectView>(targetObject).Transform;
			}

			var targetLookatRotation = Quaternion.LookRotation(moveTargetPosition.position - CharacterTransform.position);

			// Register everything here
			// Starting with the very first motion in the pattern
			var lookToAngle = Quaternion.Angle(targetLookatRotation, CharacterTransform.rotation);
			var lookToDuration = lookToAngle / 24f / speed;

			const string easeType = "linear";
			iTween.LookTo(CharacterRoot,
				iTween.Hash("looktarget", moveTargetPosition, "easetype", easeType, "time", lookToDuration));
			var walkDuration = Vector3.Distance(moveTargetPosition.localPosition, CharacterTransform.localPosition) / speed;
			iTween.MoveTo(CharacterRoot,
				iTween.Hash("position", moveTargetPosition, "easetype", easeType, "time", walkDuration, "delay", lookToDuration));

			var rotateDuration = 0f;
			if (targetObject == null)
			{
				rotateDuration =
					(lookToAngle + Quaternion.Angle(moveTargetPosition.localRotation, CharacterTransform.localRotation)) / 24f / speed;
				iTween.RotateTo(CharacterRoot,
					iTween.Hash("rotation", moveTargetPosition, "easetype", easeType, "time", rotateDuration, "delay",
						walkDuration + lookToDuration));
			}

			_viewModel.SDRoot.IntervalRunner.SubscribeToInterval(FinishedWalking, walkDuration + lookToDuration + rotateDuration,
				false);
		}

		private void InterruptMovement()
		{
			FinishedWalking();
			iTween.Stop(CharacterRoot);
		}

		private void FinishedWalking()
		{
			// Finished walking sequence
			_viewModel.SDRoot.IntervalRunner.UnsubscribeFromInterval(FinishedWalking);
			_viewModel.AnimationId.SetValue("Idle");
			_viewModel.FinishedMovement();
		}

		#endregion
	}
}