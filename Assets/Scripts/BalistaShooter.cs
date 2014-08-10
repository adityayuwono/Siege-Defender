using System;
using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.Models;
using Scripts.ViewModels;
using Scripts.Views;

namespace Scripts
{
    public class BalistaShooter : EngineBase
    {
        private readonly EngineModel _model;
        
        public BalistaShooter(EngineModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public override void MapInjections()
        {
            IoCContainer = new IoCContainer();
            ResourceManager = new ResourcePooler();

            IoCContainer.RegisterFor<PlayerViewModel>().TypeOf<BaseView>().To<PlayerView>();
            IoCContainer.RegisterFor<EnemyBaseViewModel>().TypeOf<BaseView>().To<EnemyBaseView>();
            IoCContainer.RegisterFor<ObjectViewModel>().TypeOf<BaseView>().To<ObjectView>();
            IoCContainer.RegisterFor<ProjectileViewModel>().TypeOf<BaseView>().To<ProjectileView>();
            IoCContainer.RegisterFor<ShooterViewModel>().TypeOf<BaseView>().To<ShooterView>();
            IoCContainer.RegisterFor<EnemyManagerViewModel>().TypeOf<BaseView>().To<EnemyManagerView>();
            IoCContainer.RegisterFor<TargetViewModel>().TypeOf<BaseView>().To<TargetView>();

            IoCContainer.RegisterFor<SceneViewModel>().TypeOf<BaseView>().To<SceneView>();
            IoCContainer.RegisterFor<ElementViewModel>().TypeOf<BaseView>().To<ElementView>();
            IoCContainer.RegisterFor<PlayerHitboxViewModel>().TypeOf<BaseView>().To<PlayerHitboxView>();

            IoCContainer.RegisterFor<PlayerModel>().TypeOf<ElementViewModel>().To<PlayerViewModel>();
            IoCContainer.RegisterFor<EnemyManagerModel>().TypeOf<ElementViewModel>().To<EnemyManagerViewModel>();
            IoCContainer.RegisterFor<PlayerHitboxModel>().TypeOf<ElementViewModel>().To<PlayerHitboxViewModel>();

            base.MapInjections();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var projectileModel in _model.Projectiles)
            {
                _projectiles.Add(projectileModel.Id, projectileModel);
            }

            var scene = new SceneViewModel(_model.Scene, this);
            scene.Activate();
            scene.Show();
        }

        private readonly Dictionary<string, ProjectileModel> _projectiles = new Dictionary<string, ProjectileModel>(); 
        public override ProjectileModel GetProjectileModel(string projectileId)
        {
            return _projectiles[projectileId];
        }



        public override EnemyBaseModel GetEnemy(string enemyId)
        {
            foreach (var enemyBaseModel in _model.Enemies)
            {
                if (enemyBaseModel.Id == enemyId)
                    return enemyBaseModel;
            }
            throw new EngineException(this, string.Format("Enemy not found: {0}", enemyId));
        }

        private readonly Dictionary<string, EnemyBaseViewModel> _enemies = new Dictionary<string, EnemyBaseViewModel>();
        public override void RegisterEnemy(EnemyBaseViewModel enemy)
        {
            _enemies.Add(enemy.Id, enemy);
        }

        public override void DamageEnemy(string enemyId, float damage)
        {
            if (_enemies.ContainsKey(enemyId))
            {
                var enemy = _enemies[enemyId];
                enemy.ApplyDamage(damage);
            }
        }

        public override void RemoveEnemy(ObjectViewModel enemy)
        {
            _enemies.Remove(enemy.Id);
        }


        public override LevelModel GetLevel(string levelId)
        {
            foreach (var levelModel in _model.Levels)
            {
                if (levelModel.Id == levelId)
                    return levelModel;
            }
            throw new EngineException(this, string.Format("Enemy not found: {0}", levelId));
        }

        
    }
}
