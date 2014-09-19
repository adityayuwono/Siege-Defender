using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models;
using Scripts.Models.Actions;
using Scripts.Models.Enemies;
using Scripts.Models.GUIs;
using Scripts.ViewModels;
using Scripts.ViewModels.Actions;
using Scripts.ViewModels.Enemies;
using Scripts.ViewModels.GUIs;
using Scripts.Views;
using Scripts.Views.GUIs;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts
{
    public class SiegeDefender : EngineBase
    {
        public static EngineBase Instance;

        private readonly Engine_Model _model;
        private readonly Inventory_Model _inventoryModel;

        public override Inventory_Model InventoryModel
        {
            get { return _inventoryModel; }
        }

        private readonly BalistaContext _context;

        public SiegeDefender(Engine_Model model, Inventory_Model inventoryModel, BalistaContext parent) : base(model, null)
        {
            _model = model;
            _context = parent;
            _inventoryModel = inventoryModel;

            Instance = this;
        }

        public override void MapInjections()
        {
            IoCContainer = new IoCContainer();
            Binding = new BindingManager(this);
            ResourceManager = new ResourcePooler(this);

            #region Model to ViewModel
            IoCContainer.RegisterFor<Element_Model>().TypeOf<Object>().To<ElementViewModel>();
            IoCContainer.RegisterFor<Player_Model>().TypeOf<Object>().To<PlayerViewModel>();
            IoCContainer.RegisterFor<EnemyManager_Model>().TypeOf<Object>().To<EnemyManagerViewModel>();
            IoCContainer.RegisterFor<PlayerHitbox_Model>().TypeOf<Object>().To<PlayerHitboxViewModel>();
            IoCContainer.RegisterFor<Root_GUIModel>().TypeOf<Object>().To<GUIRoot>();
            IoCContainer.RegisterFor<DamageDisplay_GUIModel>().TypeOf<Object>().To<DamageDisplayManager>();
            IoCContainer.RegisterFor<ObjectDisplay_Model>().TypeOf<Object>().To<ObjectDisplay_ViewModel>();
            // GUIs
            IoCContainer.RegisterFor<Inventory_Model>().TypeOf<Object>().To<Inventory>();
            IoCContainer.RegisterFor<Item_Model>().TypeOf<Object>().To<Item_ViewModel>();
            IoCContainer.RegisterFor<Button_GUIModel>().TypeOf<Object>().To<Button_ViewModel>();

            // ProjectileBaseViewModel
            IoCContainer.RegisterFor<Projectile_Model>().TypeOf<ProjectileBaseViewModel>().To<ProjectileViewModel>();
            IoCContainer.RegisterFor<PiercingProjectile_Model>().TypeOf<ProjectileBaseViewModel>().To<PiercingProjectile_ViewModel>();
            IoCContainer.RegisterFor<AoE_Model>().TypeOf<ProjectileBaseViewModel>().To<AoEViewModel>();
            IoCContainer.RegisterFor<ParticleAoE_Model>().TypeOf<ProjectileBaseViewModel>().To<ParticleAoEViewModel>();

            IoCContainer.RegisterFor<EnemyBaseModel>().TypeOf<EnemyBase>().To<EnemyBase>();
            IoCContainer.RegisterFor<BossModel>().TypeOf<EnemyBase>().To<Boss>();
            IoCContainer.RegisterFor<Damage_GUIModel>().TypeOf<DamageGUI>().To<DamageGUI>();

            IoCContainer.RegisterFor<Root_GUIModel>().TypeOf<ElementViewModel>().To<GUIRoot>();

            // Actions, doesnt have a view
            IoCContainer.RegisterFor<LoadScene_ActionModel>().TypeOf<BaseActionViewModel>().To<LoadSceneActionViewModel>();
            IoCContainer.RegisterFor<Setter_ActionModel>().TypeOf<BaseActionViewModel>().To<SetterActionViewModel>();
            IoCContainer.RegisterFor<Base_ConditionModel>().TypeOf<Base_ConditionViewModel>().To<Base_ConditionViewModel>();
            #endregion

            #region ViewModel to View(BaseView)
            IoCContainer.RegisterFor<ProjectileViewModel>().TypeOf<BaseView>().To<ProjectileView>();
            IoCContainer.RegisterFor<PiercingProjectile_ViewModel>().TypeOf<BaseView>().To<PiercingProjectile_View>();
            IoCContainer.RegisterFor<AoEViewModel>().TypeOf<BaseView>().To<AoEView>();
            IoCContainer.RegisterFor<ParticleAoEViewModel>().TypeOf<BaseView>().To<ParticleAoEView>();
            IoCContainer.RegisterFor<EnemyBase>().TypeOf<BaseView>().To<EnemyBaseView>();
            IoCContainer.RegisterFor<Boss>().TypeOf<BaseView>().To<Boss_View>();
            IoCContainer.RegisterFor<LabelGUI>().TypeOf<BaseView>().To<LabelGUIView>();
            IoCContainer.RegisterFor<DamageGUI>().TypeOf<BaseView>().To<DamageGUIView>();
            IoCContainer.RegisterFor<DamageDisplayManager>().TypeOf<BaseView>().To<DamageDisplayView>();
            IoCContainer.RegisterFor<ObjectDisplay_ViewModel>().TypeOf<BaseView>().To<ObjectDisplay_View>();
            // GUIs
            IoCContainer.RegisterFor<Item_ViewModel>().TypeOf<BaseView>().To<Item_View>();
            IoCContainer.RegisterFor<Inventory>().TypeOf<BaseView>().To<Inventory_View>();
            IoCContainer.RegisterFor<EquipmentSlot_ViewModel>().TypeOf<BaseView>().To<EquipmentSlot_View>();
            IoCContainer.RegisterFor<Button_ViewModel>().TypeOf<BaseView>().To<Button_View>();

            IoCContainer.RegisterFor<Object>().TypeOf<BaseView>().To<ObjectView>();
            IoCContainer.RegisterFor<StaticObject_ViewModel>().TypeOf<BaseView>().To<StaticObject_View>();
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
        


        public override Level_Model GetLevel(string levelId)
        {
            foreach (var levelModel in _model.Levels.Where(levelModel => levelModel.Id == levelId))
            {
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
                _currentScene.Destroy();// Destroy scenes when they are not needed anymore to clear memory

            // I think it's save enough to show a new one, let's hope i'm right
            _currentScene = _scenes[sceneId];
            _currentScene.Activate();
            _currentScene.Show();
        }


        public override void Save()
        {
            Serializer.SaveObjectToXML(_inventoryModel);
        }
    }
}
