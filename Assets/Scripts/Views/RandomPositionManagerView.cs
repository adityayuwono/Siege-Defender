using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
	public class RandomPositionManagerView : ElementView
	{
		protected readonly List<MinMaxRandom> SpawnPoints = new List<MinMaxRandom>();

		public RandomPositionManagerView(RandomPositionManager viewModel, ObjectView parent) : base(viewModel, parent)
		{
		}

		protected int SpawnPointCount
		{
			get { return SpawnPoints.Count; }
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			var colliders = GameObject.GetComponentsInChildren<Collider>();
			foreach (var collider in colliders)
			{
				var minMaxRandom = new MinMaxRandom(collider);
				SpawnPoints.Add(minMaxRandom);
			}
		}

		public virtual Vector3 GetRandomSpawnPoint(bool ignoreY = true, int spawnIndex = -1)
		{
			if (spawnIndex == -1)
				spawnIndex = Random.Range(0, SpawnPointCount);

			return SpawnPoints[spawnIndex].GetRandomSpot(ignoreY);
		}
	}
}