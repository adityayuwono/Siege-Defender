﻿using System.Collections.Generic;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class RandomPositionManagerView : ElementView
    {
        public RandomPositionManagerView(RandomPositionManager viewModel, ObjectView parent) : base(viewModel, parent)
        {
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

        private readonly List<MinMaxRandom> _spawnPoints = new List<MinMaxRandom>();
        protected int SpawnPoints { get { return _spawnPoints.Count; } }
        public virtual Vector3 GetRandomSpawnPoint(bool ignoreY = true, int spawnIndex=-1)
        {
            if (spawnIndex == -1)
                spawnIndex = Random.Range(0, SpawnPoints);

            return _spawnPoints[spawnIndex].GetRandomSpot(ignoreY); ;
        }
    }

    public class MinMaxRandom
    {
        private readonly Collider _collider;

        public MinMaxRandom(Collider collider)
        {
            _collider = collider;
        }

        public Vector3 GetRandomSpot(bool ignoreY=true)
        {
            var min = _collider.bounds.min;
            var max = _collider.bounds.max;
            var rX = Random.Range(min.x, max.x);
            var rY = ignoreY ? 0 : Random.Range(min.y, min.y);
            var rZ = Random.Range(min.z, max.z);

            return new Vector3(rX, rY, rZ);
        }
    }
}