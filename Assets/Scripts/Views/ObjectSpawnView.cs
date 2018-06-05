using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
	public class ObjectSpawnView : IntervalView
	{
		private readonly ObjectSpawn _viewModel;

		private int _spawnPointOverride;

		public ObjectSpawnView(ObjectSpawn viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnShow()
		{
			base.OnShow();

			for (var i = 0; i < SpawnPoints.Count; i++)
			{
				_spawnPointOverride = i;
				_viewModel.SpawnObject();
			}
		}

		public override Vector3 GetRandomSpawnPoint(bool ignoreY = true, int spawnIndex = -1)
		{
			return base.GetRandomSpawnPoint(true, _spawnPointOverride);
		}
	}
}