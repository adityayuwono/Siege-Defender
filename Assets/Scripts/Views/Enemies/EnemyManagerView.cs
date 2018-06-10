using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views.Enemies
{
	public class EnemyManagerView : IntervalView
	{
		private readonly EnemyManager _viewModel;

		public EnemyManagerView(EnemyManager viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		public override Vector3 GetRandomSpawnPoint(bool ignoreY = true, int spawnIndex = 0)
		{
			var spawnIndexOverride = _viewModel.SpawnIndexOverride;
			if (spawnIndexOverride >= SpawnPointCount)
			{
				Debug.LogError(string.Format("{0} is more than the available Spawning Points of {1}",
					_viewModel.SpawnIndexOverride, Id));
			}

			return base.GetRandomSpawnPoint(ignoreY, spawnIndexOverride);
		}

		protected override void OnShow()
		{
			base.OnShow();

			// Listen to interval changes
			_viewModel.Interval.OnChange += Interval_OnChange;

			if (string.IsNullOrEmpty(_viewModel.Level.GetValue()))
			{
				return;
			}

			StartInterval();
		}

		protected override void OnHide(string reason)
		{
			_viewModel.Interval.OnChange -= Interval_OnChange;
			StopInterval();

			base.OnHide(reason);
		}

		protected override void IntervalInvoked()
		{
			_viewModel.SpawnEnemy();
		}

		private void Interval_OnChange()
		{
			// When the interval changes we stop it and start it again
			StopInterval();
			StartInterval();
		}
	}
}