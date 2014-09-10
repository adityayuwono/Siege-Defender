using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EnemyManagerViewModel : IntervalViewModel<EnemyBaseViewModel>
    {
        private readonly EnemyManagerModel _model;

        public EnemyManagerViewModel(EnemyManagerModel model, SceneViewModel parent) : base(model, parent)
        {
            _model = model;

            _levelModel = Root.GetLevel(_model.LevelId);
        }

        private readonly Level_Model _levelModel;
        public override float Interval
        {
            get { return _levelModel.Interval; }
        }

        private int _spawnIndex;
        public void SpawnEnemy()
        {
            if (_spawnIndex >= _levelModel.SpawnSequence.Count)
                _spawnIndex = 0;

            var enemyId = _levelModel.SpawnSequence[_spawnIndex].EnemyId;
            _spawnIndex++;

            var enemy = GetObject<EnemyBaseViewModel>(enemyId);
            enemy.Activate();
            enemy.Show();
        }
    }
}
