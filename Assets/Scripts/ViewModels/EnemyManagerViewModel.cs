using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EnemyManagerViewModel : IntervalViewModel<EnemyBaseViewModel>
    {
        private readonly EnemyManager_Model _model;

        public EnemyManagerViewModel(EnemyManager_Model model, SceneViewModel parent) : base(model, parent)
        {
            _model = model;

            LoadLevel(_model.LevelId);
        }

        public void LoadLevel(string levelId)
        {
            _levelModel = Root.GetLevel(levelId);
            _currentLoop = 0;
        }

        private Level_Model _levelModel;
        public override float Interval
        {
            get { return _levelModel.Interval; }
        }

        private int _currentLoop;

        private int _spawnIndex;
        public void SpawnEnemy()
        {
            if (_spawnIndex >= _levelModel.SpawnSequence.Count && _currentLoop < _levelModel.LoopCount)
            {
                _spawnIndex = 0;
                _currentLoop++;
            }

            var enemyId = _levelModel.SpawnSequence[_spawnIndex].EnemyId;
            _spawnIndex++;

            var enemy = GetObject<EnemyBaseViewModel>(enemyId);
            enemy.Activate();
            enemy.Show();
        }
    }
}
