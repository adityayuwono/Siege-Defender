﻿using System.Collections;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.ViewModels;
using Scripts.ViewModels.Enemies;
using Scripts.ViewModels.GUIs;
using Scripts.Views;
using Scripts.Views.GUIs;
using Object = Scripts.ViewModels.Object;

namespace Scripts
{
    public class SiegeDefender : EngineBase
    {
        private readonly EngineModel _model;
        private readonly PlayerSettingsModel _playerSettingsModel;

        public override PlayerSettingsModel PlayerSettingsModel
        {
            get { return _playerSettingsModel; }
        }

        private readonly BalistaContext _context;

        public SiegeDefender(EngineModel model, PlayerSettingsModel playerSettingsModel, BalistaContext parent) : base(model, null)
        {
            _model = model;
            _context = parent;
            _playerSettingsModel = playerSettingsModel;
        }

        public override void MapInjections()
        {
            base.MapInjections();

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
            IoCContainer.RegisterFor<ProjectileItem>().TypeOf<BaseView>().To<ItemView>();
            IoCContainer.RegisterFor<Inventory>().TypeOf<BaseView>().To<InventoryView>();
            IoCContainer.RegisterFor<EquipmentSlot>().TypeOf<BaseView>().To<EquipmentSlotView>();
            IoCContainer.RegisterFor<ButtonGUI>().TypeOf<BaseView>().To<ButtonView>();
            IoCContainer.RegisterFor<ProgressBarGUI>().TypeOf<BaseView>().To<ProgressBarGUIView>();
            IoCContainer.RegisterFor<CooldownGUI>().TypeOf<BaseView>().To<CooldownGUIView>();
            IoCContainer.RegisterFor<ShooterGUI>().TypeOf<BaseView>().To<ShooterGUIView>();

            IoCContainer.RegisterFor<Object>().TypeOf<BaseView>().To<ObjectView>();
            IoCContainer.RegisterFor<SpecialEffect>().TypeOf<BaseView>().To<SpecialEffectView>();
            IoCContainer.RegisterFor<StaticObject>().TypeOf<BaseView>().To<StaticObjectView>();
            IoCContainer.RegisterFor<Shooter>().TypeOf<BaseView>().To<ShooterView>();
            IoCContainer.RegisterFor<Target>().TypeOf<BaseView>().To<TargetView>();
            IoCContainer.RegisterFor<Scene>().TypeOf<BaseView>().To<SceneView>();
            
            IoCContainer.RegisterFor<Element>().TypeOf<BaseView>().To<ElementView>();
            IoCContainer.RegisterFor<Player>().TypeOf<BaseView>().To<PlayerView>();
            IoCContainer.RegisterFor<EnemyManager>().TypeOf<BaseView>().To<EnemyManagerView>();
            IoCContainer.RegisterFor<ObjectSpawn>().TypeOf<BaseView>().To<ObjectSpawnView>();
            IoCContainer.RegisterFor<PlayerHitbox>().TypeOf<BaseView>().To<PlayerHitboxView>();

            IoCContainer.RegisterFor<GUIRoot>().TypeOf<BaseView>().To<GUIRootView>();
            #endregion
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            
            ChangeScene(_model.Scenes[0].Id);// Load the first scene on the list
        }

        public override IIntervalRunner IntervalRunner
        {
            get { return _context.IntervalRunner; }
        }

        public override void StartCoroutine(IEnumerator coroutine)
        {
            _context.StartCoroutine(coroutine);
        }

        public override void ThrowError(string message)
        {
            _context.ThrowError(message);
        }

        public override void Save()
        {
            Serializer.SaveObjectToXML(_playerSettingsModel);
        }

        #region Analytics Log

        public override void LogScreen(string screenId)
        {
            base.LogScreen(screenId);

            _context.GoogleAnalytics.LogScreen(screenId);
        }

        public override void LogEvent(string eventCategory, string eventAction, string eventLabel, long value)
        {
            base.LogEvent(eventCategory, eventAction, eventLabel, value);

            _context.GoogleAnalytics.LogEvent(eventCategory, eventAction, eventLabel, value);
        }

        #endregion
    }
}
