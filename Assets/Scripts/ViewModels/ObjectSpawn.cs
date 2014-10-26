using Scripts.Helpers;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class ObjectSpawn : Interval<Object>
    {
        private readonly ObjectSpawnModel _model;

        public ObjectSpawn(ObjectSpawnModel model, Object parent) : base(model, parent)
        {
            _model = model;

            if (string.IsNullOrEmpty(_model.LevelId))
                throw new EngineException(this, "An ObjectSpawn need a LevelId");
        }

        public override void Show()
        {
            base.Show();
        
            var levelModel = Root.GetLevel(_model.LevelId);
            
            foreach (var spawnModel in levelModel.SpawnSequence)
            {
                var objectToCache = GetObject<Object>(spawnModel.EnemyId, this);
                objectToCache.Activate(this);
                objectToCache.Show();
            }
        }
    }
}
