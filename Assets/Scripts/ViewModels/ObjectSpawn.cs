﻿using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.Models;

namespace Scripts.ViewModels
{
    /// <summary>
    /// Will spawn one of the SpawnModel on each SpawnPoint defined
    /// </summary>
    public class ObjectSpawn : Interval<Object>
    {
        private readonly ObjectSpawnModel _model;

        public ObjectSpawn(ObjectSpawnModel model, Object parent) : base(model, parent)
        {
            _model = model;

            if (string.IsNullOrEmpty(_model.LevelId))
                throw new EngineException(this, "An ObjectSpawn need a LevelId");
        }

        private readonly List<string> _objectIds = new List<string>();
        protected override void OnLoad()
        {
            base.OnLoad();

            var levelModel = SDRoot.GetLevel(_model.LevelId);
            foreach (var spawnModel in levelModel.SpawnSequence)
                _objectIds.Add(spawnModel.EnemyId);
        }
        
        public void SpawnObject()
        {
            var randomIndex = UnityEngine.Random.Range(0, _objectIds.Count);

            var randomId = _objectIds[randomIndex];

            var objectVM = GetObject<Object>(randomId, GetParent<Scene>());
            objectVM.Activate(this);
            objectVM.Show();
        }
    }
}
