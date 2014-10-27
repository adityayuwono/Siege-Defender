using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Components;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Models.Actions;
using Scripts.Models.Enemies;
using Scripts.Models.GUIs;
using Scripts.ViewModels;
using Scripts.ViewModels.Actions;
using Scripts.ViewModels.Enemies;
using Scripts.ViewModels.GUIs;
using Scripts.Views;
using Object = Scripts.ViewModels.Object;

namespace Scripts
{
    public abstract class EngineBase : Base, IContext
    {
        /// <summary>
        /// The root of all classes
        /// </summary>
        public override EngineBase Root
        {
            get { return this; }
        }

        private readonly EngineModel _model;
        
        public EngineBase(EngineModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            // Cache all scenes on the dictionary
            foreach (var sceneModel in _model.Scenes)
                _scenes.Add(sceneModel.Id, new Scene(sceneModel, this));
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            // Keep reference of every ObjectModel in a Dictionary for faster lookup
            foreach (var objectModel in _model.Objects)
                _objectModels.Add(objectModel.Id, objectModel);
        }

        #region View Lookup Pool
        private readonly Dictionary<string, BaseView> _views = new Dictionary<string, BaseView>();
        public void RegisterView(Base viewModel, BaseView view)
        {
            if (_views.ContainsKey(viewModel.FullId))
                throw new EngineException(this, string.Format("Failed to register View of Type: {1}, duplicate for Id: {0}", viewModel.Id, viewModel.GetType()));

            _views.Add(viewModel.FullId, view);
        }
        public void UnregisterView(Base viewModel)
        {
            _views.Remove(viewModel.FullId);
        }
        public T GetView<T>(Base viewModel) where T:BaseView
        {
            var id = viewModel.FullId;
            if (!_views.ContainsKey(id))
                throw new EngineException(this, string.Format("Failed to get view for Id: {0}", id));

            return _views[id] as T;
        }
        #endregion

        #region Singletons
        // Singletons, meaning there should be only one instance of these
        public abstract IntervalRunner IntervalRunner { get; }
        public IIoCContainer IoCContainer;
        public IResource ResourceManager;
        public DamageDisplayManager DamageDisplay;
        public SpecialEffectManager SpecialEffectManager;
        public PropertyLookup PropertyLookup { get; private set; }
        #endregion

        #region Object Models
        private readonly Dictionary<string, ObjectModel> _objectModels = new Dictionary<string, ObjectModel>();
        public void AddNewObjectModel(ObjectModel newModel)
        {
            _objectModels.Add(newModel.Id, newModel);
        }
        public ObjectModel GetObjectModel(string id)
        {
            if (_objectModels.ContainsKey(id))
                return _objectModels[id];

            throw new EngineException(this, string.Format("ObjectModel not found, Id: {0}", id));
        }
        #endregion

        public virtual void MapInjections()
        {
            IoCContainer = new IoCContainer();
            ResourceManager = new ResourcePooler(this);
            PropertyLookup = new PropertyLookup(this, this);// This is the root

            #region Model to ViewModel
            IoCContainer.RegisterFor<ObjectModel>().TypeOf<Object>().To<Object>();
            IoCContainer.RegisterFor<SpecialEffectModel>().TypeOf<Object>().To<SpecialEffect>();
            IoCContainer.RegisterFor<ElementModel>().TypeOf<Object>().To<Element>();
            IoCContainer.RegisterFor<PlayerModel>().TypeOf<Object>().To<Player>();
            IoCContainer.RegisterFor<EnemyManagerModel>().TypeOf<Object>().To<EnemyManager>();
            IoCContainer.RegisterFor<ObjectSpawnModel>().TypeOf<Object>().To<ObjectSpawn>();
            IoCContainer.RegisterFor<PlayerHitboxModel>().TypeOf<Object>().To<PlayerHitbox>();
            IoCContainer.RegisterFor<RootGUIModel>().TypeOf<Object>().To<GUIRoot>();
            IoCContainer.RegisterFor<DamageDisplayGUIModel>().TypeOf<Object>().To<DamageDisplayManager>();
            IoCContainer.RegisterFor<SpecialEffectManagerModel>().TypeOf<Object>().To<SpecialEffectManager>();
            IoCContainer.RegisterFor<ObjectDisplayModel>().TypeOf<Object>().To<ObjectDisplay>();
            // GUIs
            IoCContainer.RegisterFor<InventoryModel>().TypeOf<Object>().To<Inventory>();
            IoCContainer.RegisterFor<ItemModel>().TypeOf<Object>().To<Item>();
            IoCContainer.RegisterFor<ButtonGUIModel>().TypeOf<Object>().To<Button>();
            IoCContainer.RegisterFor<ShooterGUIsModel>().TypeOf<Object>().To<ShooterGUIs>();
            IoCContainer.RegisterFor<ShooterGUIModel>().TypeOf<Object>().To<ShooterGUI>();

            // ProjectileBase
            IoCContainer.RegisterFor<ProjectileModel>().TypeOf<Object>().To<Projectile>();
            IoCContainer.RegisterFor<PiercingProjectileModel>().TypeOf<Object>().To<PiercingProjectile>();
            IoCContainer.RegisterFor<AoEModel>().TypeOf<Object>().To<AoE>();
            IoCContainer.RegisterFor<ParticleAoEModel>().TypeOf<Object>().To<ParticleAoE>();

            IoCContainer.RegisterFor<EnemyBaseModel>().TypeOf<Object>().To<EnemyBase>();
            IoCContainer.RegisterFor<BossModel>().TypeOf<Object>().To<Boss>();
            IoCContainer.RegisterFor<DamageGUIModel>().TypeOf<Object>().To<DamageGUI>();

            IoCContainer.RegisterFor<RootGUIModel>().TypeOf<Element>().To<GUIRoot>();

            IoCContainer.RegisterFor<ItemModel>().TypeOf<Item>().To<Item>();
            IoCContainer.RegisterFor<ProjectileItemModel>().TypeOf<Item>().To<ProjectileItem>();

            // Actions, doesnt have a view
            IoCContainer.RegisterFor<LoadSceneActionModel>().TypeOf<BaseAction>().To<LoadSceneAction>();
            IoCContainer.RegisterFor<SetterActionModel>().TypeOf<BaseAction>().To<SetterAction>();
            IoCContainer.RegisterFor<MoveActionModel>().TypeOf<BaseAction>().To<MoveAction>();
            IoCContainer.RegisterFor<StartSpecialEventModel>().TypeOf<BaseAction>().To<StartSpecialEventAction>();
            IoCContainer.RegisterFor<ValueConditionModel>().TypeOf<BaseCondition>().To<ValueCondition>();
            IoCContainer.RegisterFor<RandomConditionModel>().TypeOf<BaseCondition>().To<RandomCondition>();
            // Triggers
            IoCContainer.RegisterFor<TriggeredModel>().TypeOf<Triggered>().To<Triggered>();
            IoCContainer.RegisterFor<EventTriggeredModel>().TypeOf<Triggered>().To<EventTriggered>();
            #endregion
        }

        #region Virtual Methods

        public virtual PlayerSettingsModel PlayerSettingsModel
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual void Save()
        {
            throw new System.NotImplementedException();
        }

        public LevelModel GetLevel(string levelId)
        {
            foreach (var levelModel in _model.Levels.Where(levelModel => levelModel.Id == levelId))
                return levelModel;
            throw new EngineException(this, string.Format("Level not found: {0}", levelId));
        }

        public virtual void StartCoroutine(IEnumerator coroutine)
        {
            coroutine.MoveNext();
        }

        public virtual void ThrowError(string message)
        {
            throw new System.NotImplementedException();
        }
        
        #endregion

        #region View Model Lookup

        public void RegisterToLookup(Base viewModel)
        {
            // TODO: Also register child elements
            if (_vmLookup.ContainsKey(viewModel.Id))
                return;

            _vmLookup.Add(viewModel.Id, viewModel);
        }

        public void UnregisterFromLookup(Base viewModel)
        {
            // TODO: Also register child elements
            if (!_vmLookup.ContainsKey(viewModel.Id))
                return;

            _vmLookup.Remove(viewModel.Id);
        }
        private readonly Dictionary<string, Base> _vmLookup = new Dictionary<string, Base>();

        public T GetViewModelAsType<T>(string id) where T : Base
        {
            if (!_vmLookup.ContainsKey(id))
                throw new EngineException(this, string.Format("ViewModel {0} is not registered", id));

            var foundViewModel = _vmLookup[id];
            if (foundViewModel == null)
                throw new EngineException(this, string.Format("ViewModel {0} is not convertable to {1}", id, typeof(T)));

            var foundViewModelAsT = foundViewModel as T;

            return foundViewModelAsT;
        }

        #endregion

        private Scene _currentScene;
        protected readonly Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();
        public Scene ChangeScene(string sceneId, string levelId = "")
        {
            // Deactivate Current Active Scene, to avoid space time continuum
            if (_currentScene != null)
                _currentScene.Destroy();// Destroy scenes when they are not needed anymore to clear memory

            // I think it's save enough to show a new one, let's hope i'm right
            _currentScene = _scenes[sceneId];
            _currentScene.Activate(levelId);
            _currentScene.Show();

            return _currentScene;
        }
    }
}
