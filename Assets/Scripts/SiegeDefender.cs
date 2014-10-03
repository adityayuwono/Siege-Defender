using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Components;
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

        private readonly EngineModel _model;
        private readonly InventoryModel _inventoryModel;

        public override InventoryModel InventoryModel
        {
            get { return _inventoryModel; }
        }

        private readonly BalistaContext _context;

        public SiegeDefender(EngineModel model, InventoryModel inventoryModel, BalistaContext parent) : base(model, null)
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
            IoCContainer.RegisterFor<ObjectModel>().TypeOf<Object>().To<Object>();
            IoCContainer.RegisterFor<SpecialEffectModel>().TypeOf<Object>().To<SpecialEffect>();
            IoCContainer.RegisterFor<ElementModel>().TypeOf<Object>().To<Element>();
            IoCContainer.RegisterFor<PlayerModel>().TypeOf<Object>().To<Player>();
            IoCContainer.RegisterFor<EnemyManagerModel>().TypeOf<Object>().To<EnemyManager>();
            IoCContainer.RegisterFor<PlayerHitboxModel>().TypeOf<Object>().To<PlayerHitbox>();
            IoCContainer.RegisterFor<RootGUIModel>().TypeOf<Object>().To<GUIRoot>();
            IoCContainer.RegisterFor<DamageDisplayGUIModel>().TypeOf<Object>().To<DamageDisplayManager>();
            IoCContainer.RegisterFor<SpecialEffectManagerModel>().TypeOf<Object>().To<SpecialEffectManager>();
            IoCContainer.RegisterFor<ObjectDisplayModel>().TypeOf<Object>().To<ObjectDisplay>();
            // GUIs
            IoCContainer.RegisterFor<InventoryModel>().TypeOf<Object>().To<Inventory>();
            IoCContainer.RegisterFor<ItemModel>().TypeOf<Object>().To<Item>();
            IoCContainer.RegisterFor<ButtonGUIModel>().TypeOf<Object>().To<Button>();

            // ProjectileBase
            IoCContainer.RegisterFor<ProjectileModel>().TypeOf<Object>().To<Projectile>();
            IoCContainer.RegisterFor<PiercingProjectileModel>().TypeOf<Object>().To<PiercingProjectile>();
            IoCContainer.RegisterFor<AoEModel>().TypeOf<Object>().To<AoE>();
            IoCContainer.RegisterFor<ParticleAoEModel>().TypeOf<Object>().To<ParticleAoE>();

            IoCContainer.RegisterFor<EnemyBaseModel>().TypeOf<Object>().To<EnemyBase>();
            IoCContainer.RegisterFor<BossModel>().TypeOf<Object>().To<Boss>();
            IoCContainer.RegisterFor<DamageGUIModel>().TypeOf<Object>().To<DamageGUI>();

            IoCContainer.RegisterFor<RootGUIModel>().TypeOf<Element>().To<GUIRoot>();

            // Actions, doesnt have a view
            IoCContainer.RegisterFor<LoadSceneActionModel>().TypeOf<BaseAction>().To<LoadSceneAction>();
            IoCContainer.RegisterFor<SetterActionModel>().TypeOf<BaseAction>().To<SetterAction>();
            IoCContainer.RegisterFor<ValueConditionModel>().TypeOf<BaseCondition>().To<ValueCondition>();
            IoCContainer.RegisterFor<RandomConditionModel>().TypeOf<BaseCondition>().To<RandomCondition>();
            #endregion

            #region ViewModel to View(BaseView)
            IoCContainer.RegisterFor<Projectile>().TypeOf<BaseView>().To<ProjectileView>();
            IoCContainer.RegisterFor<PiercingProjectile>().TypeOf<BaseView>().To<PiercingProjectileView>();
            IoCContainer.RegisterFor<AoE>().TypeOf<BaseView>().To<AoEView>();
            IoCContainer.RegisterFor<ParticleAoE>().TypeOf<BaseView>().To<ParticleAoEView>();
            IoCContainer.RegisterFor<EnemyBase>().TypeOf<BaseView>().To<EnemyBaseView>();
            IoCContainer.RegisterFor<Boss>().TypeOf<BaseView>().To<BossView>();
            IoCContainer.RegisterFor<Limb>().TypeOf<BaseView>().To<LimbView>();

            IoCContainer.RegisterFor<LabelGUI>().TypeOf<BaseView>().To<LabelGUIView>();
            IoCContainer.RegisterFor<DamageGUI>().TypeOf<BaseView>().To<DamageGUIView>();
            IoCContainer.RegisterFor<DamageDisplayManager>().TypeOf<BaseView>().To<DamageDisplayView>();
            IoCContainer.RegisterFor<SpecialEffectManager>().TypeOf<BaseView>().To<SpecialEffectManagerView>();
            IoCContainer.RegisterFor<ObjectDisplay>().TypeOf<BaseView>().To<ObjectDisplayView>();
            // GUIs
            IoCContainer.RegisterFor<Item>().TypeOf<BaseView>().To<ItemView>();
            IoCContainer.RegisterFor<Inventory>().TypeOf<BaseView>().To<InventoryView>();
            IoCContainer.RegisterFor<EquipmentSlot>().TypeOf<BaseView>().To<EquipmentSlotView>();
            IoCContainer.RegisterFor<Button>().TypeOf<BaseView>().To<ButtonView>();

            IoCContainer.RegisterFor<Object>().TypeOf<BaseView>().To<ObjectView>();
            IoCContainer.RegisterFor<SpecialEffect>().TypeOf<BaseView>().To<SpecialEffectView>();
            IoCContainer.RegisterFor<StaticObject>().TypeOf<BaseView>().To<StaticObjectView>();
            IoCContainer.RegisterFor<Shooter>().TypeOf<BaseView>().To<ShooterView>();
            IoCContainer.RegisterFor<Target>().TypeOf<BaseView>().To<TargetView>();
            IoCContainer.RegisterFor<Scene>().TypeOf<BaseView>().To<SceneView>();
            
            IoCContainer.RegisterFor<Element>().TypeOf<BaseView>().To<ElementView>();
            IoCContainer.RegisterFor<Player>().TypeOf<BaseView>().To<PlayerView>();
            IoCContainer.RegisterFor<EnemyManager>().TypeOf<BaseView>().To<EnemyManagerView>();
            IoCContainer.RegisterFor<PlayerHitbox>().TypeOf<BaseView>().To<PlayerHitboxView>();

            IoCContainer.RegisterFor<GUIRoot>().TypeOf<BaseView>().To<GUIRootView>();
            #endregion

            base.MapInjections();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            // Cache all scenes on the dictionary
            foreach (var sceneModel in _model.Scenes)
                _scenes.Add(sceneModel.Id, new Scene(sceneModel, this));

            ChangeScene(_model.Scenes[0].Id);// Load the first scene on the list
        }

        public override IntervalRunner IntervalRunner
        {
            get { return _context.IntervalRunner; }
        }

        public override LevelModel GetLevel(string levelId)
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

        private Scene _currentScene;
        private readonly Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>(); 
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
