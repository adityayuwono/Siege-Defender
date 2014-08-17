using System.Collections.Generic;
using Scripts.Components;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class EnemyManagerView : IntervalView
    {
        private readonly EnemyManagerViewModel _viewModel;

        public EnemyManagerView(EnemyManagerViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnShow()
        {
            base.OnShow();

            var colliders = GameObject.GetComponentsInChildren<Collider>();
            foreach (var collider in colliders)
            {
                var minMaxRandom = new MinMaxRandom(collider.bounds);
                _spawnPoints.Add(minMaxRandom);
            }

            StartInterval();
        }



        private readonly List<MinMaxRandom> _spawnPoints = new List<MinMaxRandom>();
        public Vector3 GetRandomSpawnPoint()
        {
            var spawnPoint = Random.Range(0, _spawnPoints.Count);
            return _spawnPoints[spawnPoint].GetRandomSpot();
        }



        protected override void IntervalInvoked()
        {
            var enemy = _viewModel.SpawnEnemy();
            enemy.Show();
        }
    }

    public class MinMaxRandom
    {
        private Vector3 _min;
        private Vector3 _max;

        public MinMaxRandom(Bounds bound)
        {
            _min = bound.min;
            _max = bound.max;
        }

        public Vector3 GetRandomSpot()
        {
            var rX = Random.Range(_min.x, _max.x);
            var rZ = Random.Range(_min.z, _max.z);

            return new Vector3(rX, 0, rZ);
        }
    }
}
