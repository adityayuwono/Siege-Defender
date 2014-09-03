using System.Collections;
using Scripts.Helpers;
using Scripts.Models;
using Scripts.Models.GUIs;
using Scripts.ViewModels;
using Scripts.ViewModels.GUIs;
using Scripts.Views;
using Scripts.Views.GUIs;
using UnityEngine;

namespace Scripts
{
    public class BalistaShooter : EngineBase
    {
        public static EngineBase Instance;

        private readonly EngineModel _model;
        private readonly BalistaContext _context;

        public BalistaShooter(EngineModel model, BalistaContext parent) : base(model, null)
        {
            _model = model;
            _context = parent;

            Instance = this;
        }

        public override void MapInjections()
        {
            IoCContainer = new IoCContainer();
            ResourceManager = new ResourcePooler();

            #region Model to ViewModel
            IoCContainer.RegisterFor<ElementModel>().TypeOf<ObjectViewModel>().To<ElementViewModel>();
            IoCContainer.RegisterFor<PlayerModel>().TypeOf<ObjectViewModel>().To<PlayerViewModel>();
            IoCContainer.RegisterFor<EnemyManagerModel>().TypeOf<ObjectViewModel>().To<EnemyManagerViewModel>();
            IoCContainer.RegisterFor<PlayerHitboxModel>().TypeOf<ObjectViewModel>().To<PlayerHitboxViewModel>();
            IoCContainer.RegisterFor<GUIRootModel>().TypeOf<ObjectViewModel>().To<GUIRoot>();
            IoCContainer.RegisterFor<DamageDisplayModel>().TypeOf<ObjectViewModel>().To<DamageDisplayManager>();

            // ProjectileBaseViewModel
            IoCContainer.RegisterFor<ProjectileModel>().TypeOf<ProjectileBaseViewModel>().To<ProjectileViewModel>();
            IoCContainer.RegisterFor<PiercingProjectile_Model>().TypeOf<ProjectileBaseViewModel>().To<PiercingProjectile_ViewModel>();
            IoCContainer.RegisterFor<AoEModel>().TypeOf<ProjectileBaseViewModel>().To<AoEViewModel>();
            IoCContainer.RegisterFor<ParticleAoEModel>().TypeOf<ProjectileBaseViewModel>().To<ParticleAoEViewModel>();

            IoCContainer.RegisterFor<EnemyBaseModel>().TypeOf<EnemyBaseViewModel>().To<EnemyBaseViewModel>();
            IoCContainer.RegisterFor<DamageGUIModel>().TypeOf<DamageGUI>().To<DamageGUI>();

            IoCContainer.RegisterFor<GUIRootModel>().TypeOf<ElementViewModel>().To<GUIRoot>();
            #endregion

            #region ViewModel to View(BaseView)
            IoCContainer.RegisterFor<ProjectileViewModel>().TypeOf<BaseView>().To<ProjectileView>();
            IoCContainer.RegisterFor<PiercingProjectile_ViewModel>().TypeOf<BaseView>().To<PiercingProjectile_View>();
            IoCContainer.RegisterFor<AoEViewModel>().TypeOf<BaseView>().To<AoEView>();
            IoCContainer.RegisterFor<ParticleAoEViewModel>().TypeOf<BaseView>().To<ParticleAoEView>();
            IoCContainer.RegisterFor<EnemyBaseViewModel>().TypeOf<BaseView>().To<EnemyBaseView>();
            IoCContainer.RegisterFor<LabelGUI>().TypeOf<BaseView>().To<LabelGUIView>();
            IoCContainer.RegisterFor<DamageGUI>().TypeOf<BaseView>().To<DamageGUIView>();
            IoCContainer.RegisterFor<DamageDisplayManager>().TypeOf<BaseView>().To<DamageDisplayView>();

            IoCContainer.RegisterFor<ObjectViewModel>().TypeOf<BaseView>().To<ObjectView>();
            IoCContainer.RegisterFor<ShooterViewModel>().TypeOf<BaseView>().To<ShooterView>();
            IoCContainer.RegisterFor<TargetViewModel>().TypeOf<BaseView>().To<TargetView>();
            IoCContainer.RegisterFor<SceneViewModel>().TypeOf<BaseView>().To<SceneView>();
            
            IoCContainer.RegisterFor<ElementViewModel>().TypeOf<BaseView>().To<ElementView>();
            IoCContainer.RegisterFor<PlayerViewModel>().TypeOf<BaseView>().To<PlayerView>();
            IoCContainer.RegisterFor<EnemyManagerViewModel>().TypeOf<BaseView>().To<EnemyManagerView>();
            IoCContainer.RegisterFor<PlayerHitboxViewModel>().TypeOf<BaseView>().To<PlayerHitboxView>();

            IoCContainer.RegisterFor<GUIRoot>().TypeOf<BaseView>().To<GUIRootView>();
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

        public override void ThrowError(string message)
        {
            _context.ThrowError(message);
        }
    }
}
