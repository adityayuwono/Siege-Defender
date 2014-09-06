using System.Collections;
using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.Models;
using Scripts.Models.GUIs;
using Scripts.ViewModels;
using Scripts.ViewModels.Actions;
using Scripts.ViewModels.GUIs;
using Scripts.Views;
using Scripts.Views.GUIs;
using UnityEngine;

namespace Scripts
{
    public class SiegeDefender : EngineBase
    {
        public static EngineBase Instance;

        private readonly EngineModel _model;
        private readonly BalistaContext _context;

        public SiegeDefender(EngineModel model, BalistaContext parent) : base(model, null)
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
            // GUIs
            IoCContainer.RegisterFor<Button_Model>().TypeOf<ObjectViewModel>().To<Button_ViewModel>();

            // ProjectileBaseViewModel
            IoCContainer.RegisterFor<ProjectileModel>().TypeOf<ProjectileBaseViewModel>().To<ProjectileViewModel>();
            IoCContainer.RegisterFor<PiercingProjectile_Model>().TypeOf<ProjectileBaseViewModel>().To<PiercingProjectile_ViewModel>();
            IoCContainer.RegisterFor<AoEModel>().TypeOf<ProjectileBaseViewModel>().To<AoEViewModel>();
            IoCContainer.RegisterFor<ParticleAoEModel>().TypeOf<ProjectileBaseViewModel>().To<ParticleAoEViewModel>();

            IoCContainer.RegisterFor<EnemyBaseModel>().TypeOf<EnemyBaseViewModel>().To<EnemyBaseViewModel>();
            IoCContainer.RegisterFor<DamageGUIModel>().TypeOf<DamageGUI>().To<DamageGUI>();

            IoCContainer.RegisterFor<GUIRootModel>().TypeOf<ElementViewModel>().To<GUIRoot>();

            // Actions
            IoCContainer.RegisterFor<LoadScene_ActionModel>().TypeOf<Action_ViewModel>().To<LoadScene_ActionViewModel>();
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
            // GUIs
            IoCContainer.RegisterFor<Button_ViewModel>().TypeOf<BaseView>().To<Button_View>();

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

            // Cache all scenes on the dictionary
            foreach (var sceneModel in _model.Scenes)
                _scenes.Add(sceneModel.Id, new SceneViewModel(sceneModel, this));

            ChangeScene(_model.Scenes[0].Id);// Load the first scene on the list
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

        private SceneViewModel _currentScene;
        private readonly Dictionary<string, SceneViewModel> _scenes = new Dictionary<string, SceneViewModel>(); 
        public override void ChangeScene(string sceneId)
        {
            // Deactivate Current Active Scene, to avoid space time continuum
            if (_currentScene != null)
            {
                var reason = "Change to scene: " + sceneId;
                _currentScene.Hide(reason);
            }

            // I think it's save enough to show a new one, let's hope i'm right
            _currentScene = _scenes[sceneId];
            _currentScene.Activate();
            _currentScene.Show();
        }
    }
}
