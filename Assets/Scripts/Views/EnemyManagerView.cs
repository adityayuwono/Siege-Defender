using System;
using System.Collections.Generic;
using Scripts.ViewModels;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Views
{
    public class EnemyManagerView : IntervalView
    {
        private readonly EnemyManagerViewModel _viewModel;

        public EnemyManagerView(EnemyManagerViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            var colliders = GameObject.GetComponentsInChildren<Collider>();
            foreach (var collider in colliders)
            {
                var minMaxRandom = new MinMaxRandom(collider.bounds);
                _spawnPoints.Add(minMaxRandom);
            }
        }

        protected override void OnShow()
        {
            base.OnShow();

            // Listen to interval changes
            _viewModel.Interval.OnChange+= Interval_OnChange;
            StartInterval();
        }

        private void Interval_OnChange()
        {
            // When the interval changes we stop it and start it again
            StopInterval();
            StartInterval();
        }

        protected override void OnHide(string reason)
        {
            _viewModel.Interval.OnChange -= Interval_OnChange;
            StopInterval();

            base.OnHide(reason);
        }


        private readonly List<MinMaxRandom> _spawnPoints = new List<MinMaxRandom>();
        public Vector3 GetRandomSpawnPoint()
        {
            var spawnPoint = Random.Range(0, _spawnPoints.Count);
            return _spawnPoints[spawnPoint].GetRandomSpot();
        }



        protected override void IntervalInvoked()
        {
            _viewModel.SpawnEnemy();
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
