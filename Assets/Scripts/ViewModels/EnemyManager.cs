using System;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class EnemyManager : Interval<EnemyBase>
    {
        private readonly EnemyManagerModel _model;

        public EnemyManager(EnemyManagerModel model, Object parent) : base(model, parent)
        {
            _model = model;

            // Tell the parent Scene that it has an EnemyManager
            var parentScene = parent as Scene;
            if (parentScene != null)
                parentScene.EnemyManager = this;

            Level = new AdjustableProperty<string>("Level", this, true);
            Level.OnChange += LoadLevel;

            Level.SetValue(_model.LevelId);
        }

        public override void Hide(string reason)
        {
            Hide(reason, false);
        }

        public readonly AdjustableProperty<string> Level;

        private void LoadLevel()
        {
            var levelId = Level.GetValue();
            if (string.IsNullOrEmpty(levelId)) return;
            _spawnIndex = 0;
            _currentLoop = 0;
            _levelModel = Root.GetLevel(Level.GetValue());

            // If Loop Count is -1, we just assign MaxValue, hoping the player will never reach it
            _loopCount = _levelModel.LoopCount == -1 ? int.MaxValue : _levelModel.LoopCount;
            Interval.SetValue(_levelModel.Interval);
        }
        
        #region Spawning Enemies
        private LevelModel _levelModel;
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

            // Ignore if we have reaced the end of the sequence
            if (_spawnIndex >= _levelModel.SpawnSequence.Count) return;

            var spawnModel = _levelModel.SpawnSequence[_spawnIndex];
            var enemyId = spawnModel.EnemyId;

            // Empty enemyId mean that we want to skip some spawn iterations
            if (string.IsNullOrEmpty(enemyId)) return;

            var count = spawnModel.Count;
            SpawnIndexOverride = spawnModel.SpawnIndexOverride;
            _spawnIndex++;

            for (var i = 0; i < count; i++)
            {
                var enemy = GetObject<EnemyBase>(enemyId, GetParent<Scene>());
                enemy.Activate(this);
                enemy.Show();
            }
            SpawnIndexOverride = -1;
        }

        /// <summary>
        /// Overrides the nex spawn index used, -1 means no override
        /// </summary>
        public int SpawnIndexOverride { get; private set; }

        #endregion
    }
}
