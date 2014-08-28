using System.Collections;
using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.Models;
using Scripts.ViewModels;
using Scripts.Views;
using UnityEngine;

namespace Scripts
{
    public class BalistaShooter : EngineBase
    {
        private readonly EngineModel _model;
        private readonly BalistaContext _context;

        public BalistaShooter(EngineModel model, BalistaContext parent) : base(model, null)
        {
            _model = model;
            _context = parent;
        }

        public override void MapInjections()
        {
            IoCContainer = new IoCContainer();
            ResourceManager = new ResourcePooler();

            #region Model to ViewModel
            IoCContainer.RegisterFor<ElementModel>().TypeOf<ElementViewModel>().To<ElementViewModel>();
            IoCContainer.RegisterFor<PlayerModel>().TypeOf<ElementViewModel>().To<PlayerViewModel>();
            IoCContainer.RegisterFor<EnemyManagerModel>().TypeOf<ElementViewModel>().To<EnemyManagerViewModel>();
            IoCContainer.RegisterFor<PlayerHitboxModel>().TypeOf<ElementViewModel>().To<PlayerHitboxViewModel>();

            IoCContainer.RegisterFor<ProjectileModel>().TypeOf<ProjectileBaseViewModel>().To<ProjectileViewModel>();
            IoCContainer.RegisterFor<AoEModel>().TypeOf<ProjectileBaseViewModel>().To<AoEViewModel>();
            IoCContainer.RegisterFor<ParticleAoEModel>().TypeOf<ProjectileBaseViewModel>().To<ParticleAoEViewModel>();
            IoCContainer.RegisterFor<EnemyBaseModel>().TypeOf<EnemyBaseViewModel>().To<EnemyBaseViewModel>();
            #endregion

            #region ViewModel to View
            IoCContainer.RegisterFor<ProjectileViewModel>().TypeOf<BaseView>().To<ProjectileView>();
            IoCContainer.RegisterFor<AoEViewModel>().TypeOf<BaseView>().To<AoEView>();
            IoCContainer.RegisterFor<ParticleAoEViewModel>().TypeOf<BaseView>().To<ParticleAoEView>();
            IoCContainer.RegisterFor<EnemyBaseViewModel>().TypeOf<BaseView>().To<EnemyBaseView>();

            IoCContainer.RegisterFor<ObjectViewModel>().TypeOf<BaseView>().To<ObjectView>();
            IoCContainer.RegisterFor<ShooterViewModel>().TypeOf<BaseView>().To<ShooterView>();
            IoCContainer.RegisterFor<TargetViewModel>().TypeOf<BaseView>().To<TargetView>();
            IoCContainer.RegisterFor<SceneViewModel>().TypeOf<BaseView>().To<SceneView>();
            
            IoCContainer.RegisterFor<ElementViewModel>().TypeOf<BaseView>().To<ElementView>();
            IoCContainer.RegisterFor<PlayerViewModel>().TypeOf<BaseView>().To<PlayerView>();
            IoCContainer.RegisterFor<EnemyManagerViewModel>().TypeOf<BaseView>().To<EnemyManagerView>();
            IoCContainer.RegisterFor<PlayerHitboxViewModel>().TypeOf<BaseView>().To<PlayerHitboxView>();
            #endregion

            base.MapInjections();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            var scene = new SceneViewModel(_model.Scene, this);
            scene.Activate();
            scene.Show();
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
                enemy.ApplyDamage(damage, null);
            }
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

        public override Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return _context.StartCoroutine(coroutine);
        }
    }
}
