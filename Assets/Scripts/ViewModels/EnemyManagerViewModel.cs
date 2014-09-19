using Scripts.Core;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EnemyManagerViewModel : IntervalViewModel<EnemyBaseViewModel>
    {
        private readonly EnemyManager_Model _model;

        public EnemyManagerViewModel(EnemyManager_Model model, Object parent) : base(model, parent)
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
            _currentLoop = 0;
            _levelModel = Root.GetLevel(Level.GetValue());
            // If Loop Count is -1, we just assign MaxValue, hoping the player will never reach it
            _loopCount = _levelModel.LoopCount==-1?int.MaxValue:_levelModel.LoopCount;
            Interval.SetValue(_levelModel.Interval);
        }

        #region Spawning Enemies
        private Level_Model _levelModel;
        private int _loopCount;
        private int _currentLoop;
        private int _spawnIndex;
        public void SpawnEnemy()
        {
            // Check if we are at the end of the sequence
            if (_spawnIndex >= _levelModel.SpawnSequence.Count && _currentLoop < _loopCount)
            {
                _spawnIndex = 0;
                _currentLoop++;
            }

            // If we are supposed to be spawning enemies still
            if (_spawnIndex < _levelModel.SpawnSequence.Count)
            {
                var enemyId = _levelModel.SpawnSequence[_spawnIndex].EnemyId;
                _spawnIndex++;

                var enemy = GetObject<EnemyBaseViewModel>(enemyId);
                enemy.Activate();
                enemy.Show();
            }
        }
        #endregion
    }
}
