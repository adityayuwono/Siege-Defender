using Scripts.Models;
using Scripts.Models.Actions;
using Scripts.Models.Enemies;
using Scripts.Models.GUIs;
using Scripts.Models.Items;
using Scripts.Models.Weapons;
using Scripts.ViewModels;
using Scripts.ViewModels.Actions;
using Scripts.ViewModels.Enemies;
using Scripts.ViewModels.GUIs;
using Scripts.ViewModels.Items;
using Scripts.ViewModels.Weapons;
using Scripts.Views;
using Scripts.Views.Enemies;
using Scripts.Views.GUIs;
using Scripts.Views.Items;
using Scripts.Views.Weapons;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Components
{
	/// <summary>
	///     Main Component as a bridge between the Engine to Unity
	/// </summary>
	public class IoCBootstrapper : MonoBehaviour
	{
		protected void Start()
		{
			MapInjections();
		}

		private static void MapInjections()
		{
			#region Model to ViewModel

			IoC.IoCContainer.RegisterFor<ObjectModel>().TypeOf<Object>().To<Object>();
			IoC.IoCContainer.RegisterFor<SpecialEffectModel>().TypeOf<Object>().To<SpecialEffect>();
			IoC.IoCContainer.RegisterFor<ElementModel>().TypeOf<Object>().To<Element>();
			IoC.IoCContainer.RegisterFor<PlayerModel>().TypeOf<Object>().To<Player>();
			IoC.IoCContainer.RegisterFor<EnemyManagerModel>().TypeOf<Object>().To<EnemyManager>();
			IoC.IoCContainer.RegisterFor<ObjectSpawnModel>().TypeOf<Object>().To<ObjectSpawn>();
			IoC.IoCContainer.RegisterFor<PlayerHitboxModel>().TypeOf<Object>().To<PlayerHitbox>();
			IoC.IoCContainer.RegisterFor<RootGUIModel>().TypeOf<Object>().To<GUIRoot>();
			IoC.IoCContainer.RegisterFor<DamageDisplayGUIModel>().TypeOf<Object>().To<DamageDisplayManager>();
			IoC.IoCContainer.RegisterFor<SpecialEffectManagerModel>().TypeOf<Object>().To<SpecialEffectManager>();
			IoC.IoCContainer.RegisterFor<ObjectDisplayModel>().TypeOf<Object>().To<ObjectDisplay>();
			
			// Items
			IoC.IoCContainer.RegisterFor<InventoryModel>().TypeOf<Object>().To<Inventory>();
			IoC.IoCContainer.RegisterFor<ItemModel>().TypeOf<Object>().To<Item>();
			IoC.IoCContainer.RegisterFor<ItemModel>().TypeOf<Item>().To<Item>();
			IoC.IoCContainer.RegisterFor<ProjectileItemModel>().TypeOf<Item>().To<ProjectileItem>();
			IoC.IoCContainer.RegisterFor<EnchantmentItemModel>().TypeOf<Item>().To<EnchantmentItem>();
			IoC.IoCContainer.RegisterFor<EquipmentSlotModel>().TypeOf<EquipmentSlot>().To<EquipmentSlot>();
			IoC.IoCContainer.RegisterFor<WeaponSlotModel>().TypeOf<EquipmentSlot>().To<WeaponSlot>();

			// GUIs
			IoC.IoCContainer.RegisterFor<RootGUIModel>().TypeOf<Element>().To<GUIRoot>();
			IoC.IoCContainer.RegisterFor<ButtonGUIModel>().TypeOf<Object>().To<ButtonGUI>();
			IoC.IoCContainer.RegisterFor<ProgressBarGUIModel>().TypeOf<Object>().To<ProgressBarGUI>();
			IoC.IoCContainer.RegisterFor<CooldownGUIModel>().TypeOf<Object>().To<CooldownGUI>();
			IoC.IoCContainer.RegisterFor<ShooterGUIModel>().TypeOf<Object>().To<ShooterGUI>();
			IoC.IoCContainer.RegisterFor<LabelGUIModel>().TypeOf<Object>().To<LabelGUI>();
			IoC.IoCContainer.RegisterFor<GameEndStatsModel>().TypeOf<Object>().To<ViewModels.GUIs.GameEndStats>();

			// ProjectileBase
			IoC.IoCContainer.RegisterFor<ProjectileModel>().TypeOf<Object>().To<Projectile>();
			IoC.IoCContainer.RegisterFor<PiercingProjectileModel>().TypeOf<Object>().To<PiercingProjectile>();
			IoC.IoCContainer.RegisterFor<AoEModel>().TypeOf<Object>().To<AoE>();
			IoC.IoCContainer.RegisterFor<ParticleAoEModel>().TypeOf<Object>().To<ParticleAoE>();

			// Enemies
			IoC.IoCContainer.RegisterFor<StaticEnemyModel>().TypeOf<Object>().To<StaticEnemy>();
			IoC.IoCContainer.RegisterFor<EnemyModel>().TypeOf<Object>().To<Enemy>();
			IoC.IoCContainer.RegisterFor<BossModel>().TypeOf<Object>().To<Boss>();
			IoC.IoCContainer.RegisterFor<DamageGUIModel>().TypeOf<Object>().To<DamageGUI>();
			IoC.IoCContainer.RegisterFor<HealthBarModel>().TypeOf<Object>().To<HealthBar>();

			// Actions, doesnt have a view
			IoC.IoCContainer.RegisterFor<LoadSceneActionModel>().TypeOf<BaseAction>().To<LoadSceneAction>();
			IoC.IoCContainer.RegisterFor<SetterActionModel>().TypeOf<BaseAction>().To<SetterAction>();
			IoC.IoCContainer.RegisterFor<MoveActionModel>().TypeOf<BaseAction>().To<MoveAction>();
			IoC.IoCContainer.RegisterFor<StartSpecialEventModel>().TypeOf<BaseAction>().To<StartSpecialEventAction>();
			IoC.IoCContainer.RegisterFor<SpecialEffectActionModel>().TypeOf<BaseAction>().To<SpecialEffectAction>();
			IoC.IoCContainer.RegisterFor<CreateItemActionModel>().TypeOf<BaseAction>().To<CreateItemAction>();
			IoC.IoCContainer.RegisterFor<ValueConditionModel>().TypeOf<BaseCondition>().To<ValueCondition>();
			IoC.IoCContainer.RegisterFor<RandomConditionModel>().TypeOf<BaseCondition>().To<RandomCondition>();

			// Triggers
			IoC.IoCContainer.RegisterFor<TriggeredModel>().TypeOf<Triggered>().To<Triggered>();
			IoC.IoCContainer.RegisterFor<EventTriggeredModel>().TypeOf<Triggered>().To<EventTriggered>();

			#endregion

			#region ViewModel to View(BaseView)

			IoC.IoCContainer.RegisterFor<Projectile>().TypeOf<BaseView>().To<ProjectileView>();
			IoC.IoCContainer.RegisterFor<PiercingProjectile>().TypeOf<BaseView>().To<PiercingProjectileView>();
			IoC.IoCContainer.RegisterFor<AoE>().TypeOf<BaseView>().To<AoEView>();
			IoC.IoCContainer.RegisterFor<ParticleAoE>().TypeOf<BaseView>().To<ParticleAoEView>();

			IoC.IoCContainer.RegisterFor<StaticEnemy>().TypeOf<BaseView>().To<StaticEnemyView>();
			IoC.IoCContainer.RegisterFor<Enemy>().TypeOf<BaseView>().To<EnemyView>();
			IoC.IoCContainer.RegisterFor<Boss>().TypeOf<BaseView>().To<BossView>();
			IoC.IoCContainer.RegisterFor<Limb>().TypeOf<BaseView>().To<LimbView>();

			IoC.IoCContainer.RegisterFor<LabelGUI>().TypeOf<BaseView>().To<LabelGUIView>();
			IoC.IoCContainer.RegisterFor<DamageGUI>().TypeOf<BaseView>().To<DamageGUIView>();
			IoC.IoCContainer.RegisterFor<HealthBar>().TypeOf<BaseView>().To<HealthBarView>();
			IoC.IoCContainer.RegisterFor<DamageDisplayManager>().TypeOf<BaseView>().To<DamageDisplayView>();
			IoC.IoCContainer.RegisterFor<SpecialEffectManager>().TypeOf<BaseView>().To<SpecialEffectManagerView>();
			IoC.IoCContainer.RegisterFor<ObjectDisplay>().TypeOf<BaseView>().To<ObjectDisplayView>();

			// Items
			IoC.IoCContainer.RegisterFor<Item>().TypeOf<BaseView>().To<ItemView>();
			IoC.IoCContainer.RegisterFor<ProjectileItem>().TypeOf<BaseView>().To<ItemView>();
			IoC.IoCContainer.RegisterFor<EnchantmentItem>().TypeOf<BaseView>().To<ItemView>();
			IoC.IoCContainer.RegisterFor<Inventory>().TypeOf<BaseView>().To<InventoryView>();
			IoC.IoCContainer.RegisterFor<EquipmentSlot>().TypeOf<BaseView>().To<EquipmentSlotView>();
			IoC.IoCContainer.RegisterFor<WeaponSlot>().TypeOf<BaseView>().To<WeaponSlotView>();

			// GUIs
			IoC.IoCContainer.RegisterFor<GUIRoot>().TypeOf<BaseView>().To<GUIRootView>();

			IoC.IoCContainer.RegisterFor<ButtonGUI>().TypeOf<BaseView>().To<ButtonView>();
			IoC.IoCContainer.RegisterFor<ProgressBarGUI>().TypeOf<BaseView>().To<ProgressBarGUIView>();
			IoC.IoCContainer.RegisterFor<CooldownGUI>().TypeOf<BaseView>().To<CooldownGUIView>();
			IoC.IoCContainer.RegisterFor<ShooterGUI>().TypeOf<BaseView>().To<ShooterGUIView>();
			IoC.IoCContainer.RegisterFor<ViewModels.GUIs.GameEndStats>().TypeOf<BaseView>().To<GameEndStatsView>();

			IoC.IoCContainer.RegisterFor<Object>().TypeOf<BaseView>().To<ObjectView>();
			IoC.IoCContainer.RegisterFor<SpecialEffect>().TypeOf<BaseView>().To<SpecialEffectView>();
			IoC.IoCContainer.RegisterFor<StaticObject>().TypeOf<BaseView>().To<StaticObjectView>();
			IoC.IoCContainer.RegisterFor<Shooter>().TypeOf<BaseView>().To<ShooterView>();
			IoC.IoCContainer.RegisterFor<Target>().TypeOf<BaseView>().To<TargetView>();
			IoC.IoCContainer.RegisterFor<Scene>().TypeOf<BaseView>().To<SceneView>();

			IoC.IoCContainer.RegisterFor<Element>().TypeOf<BaseView>().To<ElementView>();
			IoC.IoCContainer.RegisterFor<Player>().TypeOf<BaseView>().To<PlayerView>();
			IoC.IoCContainer.RegisterFor<EnemyManager>().TypeOf<BaseView>().To<EnemyManagerView>();
			IoC.IoCContainer.RegisterFor<ObjectSpawn>().TypeOf<BaseView>().To<ObjectSpawnView>();
			IoC.IoCContainer.RegisterFor<PlayerHitbox>().TypeOf<BaseView>().To<PlayerHitboxView>();
			
			#endregion
		}
	}
}