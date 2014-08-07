using System;
using Scripts.Helpers;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class SpawnViewModel : BaseViewModel
    {
        private readonly SpawnModel _model;
        private readonly EnemyManagerViewModel _parent;

        public SpawnViewModel(SpawnModel model, EnemyManagerViewModel parent) : base(model, parent)
        {
            _model = model;
            _parent = parent;
        }

        public EnemyBaseViewModel Spawn()
        {
            var enemyModelToCopy = Root.GetEnemy(_model.EnemyId);

            var enemyModel = Copier.CopyAs<EnemyBaseModel>(enemyModelToCopy);
            enemyModel.Type = enemyModel.Id;
            enemyModel.Id = Guid.NewGuid().ToString();

            var newEnemy = new EnemyBaseViewModel(enemyModel, _parent);

            return newEnemy;
        }
    }
}
