using System.Collections.Generic;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EnemyManagerViewModel : IntervalViewModel
    {
        private readonly EnemyManagerModel _model;

        public EnemyManagerViewModel(EnemyManagerModel model, SceneViewModel parent) : base(model, parent)
        {
            _model = model;

            _levelModel = Root.GetLevel(_model.LevelId);
            foreach (var spawnModel in _levelModel.SpawnSequence)
            {
                var spawnVM = new SpawnViewModel(spawnModel, this);
                _spawnSequence.Add(spawnVM);
            }
        }

        private LevelModel _levelModel;
        public override float Interval
        {
            get { return _levelModel.Interval; }
        }

        private int _spawnIndex;
        private readonly List<SpawnViewModel> _spawnSequence = new List<SpawnViewModel>();

        public EnemyBaseViewModel SpawnEnemy()
        {
            if (_spawnIndex >= _spawnSequence.Count)
                _spawnIndex = 0;

            var enemy = _spawnSequence[_spawnIndex].Spawn();
            _spawnIndex++;

            enemy.DoDestroy += RemoveEnemy;
            enemy.Activate();

            Root.RegisterEnemy(enemy);

            return enemy;
        }

        private void RemoveEnemy(ObjectViewModel enemy)
        {
            Root.RemoveEnemy(enemy);
        }
    }
}
