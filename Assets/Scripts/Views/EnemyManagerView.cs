using System.Collections.Generic;
using Scripts.ViewModels;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Views
{
    public class EnemyManagerView : IntervalView
    {
        private readonly EnemyManager _viewModel;

        public EnemyManagerView(EnemyManager viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            var colliders = GameObject.GetComponentsInChildren<Collider>();
            foreach (var collider in colliders)
            {
                var minMaxRandom = new MinMaxRandom(collider);
                _spawnPoints.Add(minMaxRandom);
            }
        }

        protected override void OnShow()
        {
            base.OnShow();

            // Listen to interval changes
            _viewModel.Interval.OnChange+= Interval_OnChange;
            
            if (string.IsNullOrEmpty(_viewModel.Level.GetValue())) return;
            
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
            var spawnPointIncex = _viewModel.SpawnIndexOverride;
            if (spawnPointIncex >= _spawnPoints.Count)
                Debug.LogError(string.Format("{0} is more than the available Spawning Points of {1}", _viewModel.SpawnIndexOverride, Id));

            if (spawnPointIncex > -1)
                spawnPointIncex = Random.Range(0, _spawnPoints.Count);
            return _spawnPoints[spawnPointIncex].GetRandomSpot();
        }



        protected override void IntervalInvoked()
        {
            _viewModel.SpawnEnemy();
        }
    }

    public class MinMaxRandom
    {
        private readonly Collider _collider;

        public MinMaxRandom(Collider collider)
        {
            _collider = collider;
        }

        public Vector3 GetRandomSpot()
        {
            var min = _collider.bounds.min;
            var max = _collider.bounds.max;
            var rX = Random.Range(min.x, max.x);
            var rZ = Random.Range(min.z, max.z);

            return new Vector3(rX, 0, rZ);
        }
    }
}
