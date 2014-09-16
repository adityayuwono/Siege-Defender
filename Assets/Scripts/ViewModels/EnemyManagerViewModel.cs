using Scripts.Core;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EnemyManagerViewModel : IntervalViewModel<EnemyBaseViewModel>
    {
        private readonly EnemyManager_Model _model;

        public EnemyManagerViewModel(EnemyManager_Model model, SceneViewModel parent) : base(model, parent)
        {
            _model = model;

            Level = new AdjustableProperty<string>("Level", this);
            Level.OnChange += LoadLevel;

            Level.SetValue(_model.LevelId);
        }

        public AdjustableProperty<string> Level;

        private void LoadLevel()
        {
            _spawnIndex = 0;
            _levelModel = Root.GetLevel(Level.GetValue());
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
            if (_spawnIndex >= _levelModel.SpawnSequence.Count && (_levelModel.LoopCount < 0 || _currentLoop < _levelModel.LoopCount))
            {
                _spawnIndex = 0;
                _currentLoop++;
            }

            if (_spawnIndex < _levelModel.SpawnSequence.Count)
            {
                var enemyId = _levelModel.SpawnSequence[_spawnIndex].EnemyId;
                _spawnIndex++;

                var enemy = GetObject<EnemyBaseViewModel>(enemyId);
                enemy.Activate();
                enemy.Show();
            }
        }
    }
}
