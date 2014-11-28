using System;
using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Core;
using Scripts.Models;
using Scripts.Models.Actions;
using Scripts.Models.Enemies;
using Scripts.ViewModels;
using Scripts.ViewModels.Enemies;

namespace SiegeDefenderTests.Tests
{
    [TestFixture]
    public class PropertyLookupTests : EngineBaseStub
    {
        [Test]
        public void SimpleLookup()
        {
            var enemyId = "Enemy_" + Guid.NewGuid();
            var enemy = new EnemyBase(new EnemyBaseModel
            {
                Id = enemyId,
                Type = "TestBoss",
                AssetId = "TestEnemy"
            }, EngineBase);
            enemy.Activate();
            enemy.Show();

            var enemyHealth = EngineBase.PropertyLookup.GetProperty("{" + enemyId + ".This.Health}") as Property<float>;

            Assert.AreEqual(1, enemyHealth.GetValue());
        }

        [Test]
        public void NestedLookup()
        {
            var sceneId = "Scene_" + Guid.NewGuid();
            var inventoryId = "Inventory_" + Guid.NewGuid();
            var inventorySourceId = "InventorySource_" + Guid.NewGuid();
            var slotId = "Slot_" + Guid.NewGuid();
            var testItemId = "Item_" + Guid.NewGuid();

            var playerSettingsModel = new PlayerSettingsModel
            {
                Inventories = new List<InventoryModel>
                {
                    new InventoryModel
                    {
                        Id = inventorySourceId,
                        EquipmentSlots = new List<EquipmentSlotModel>
                        {
                            new EquipmentSlotModel
                            {
                                Id = slotId,
                                AssetId = "TestSlot",
                                Item = new ItemModel
                                {
                                    Base = testItemId,
                                    AssetId = "TestItem"
                                }
                            }
                        }
                    }
                }
            };
            EngineBase.SetPlayerSettingsModel(playerSettingsModel);
            
            var sceneModel = new SceneModel
            {
                Id = sceneId,
                AssetId = "TestScene",
                ElementsSerialized = new List<ElementModel>
                {
                    new InventoryModel
                    {
                        Id = inventoryId,
                        AssetId = "TestInventory",
                        Source = inventorySourceId,
                    }
                }
            };

            var scene = new Scene(sceneModel, EngineBase);
            scene.Activate();
            scene.Show();

            var pathToBind = string.Format("{0}.{1}.{2}", inventoryId, slotId, "ItemId");

            //var enemyHealth = EngineBase.PropertyLookup.GetProperty<float>("{"+enemyId + ".Health}");
            var itemId = EngineBase.PropertyLookup.GetProperty("{"+pathToBind+"}") as Property<string>;

            Assert.AreEqual(testItemId, itemId.GetValue());
        }

        [Test]
        public void InternalContextLookup()
        {
            var skillId = "Skill_" + Guid.NewGuid();
            var bossModel = new BossModel
            {
                Type = "TestBoss",
                AssetId = "TestBoss",
                Skills = new List<SkillModel>
                {
                    new SkillModel
                    {
                        Id = skillId,
                        Actions = new List<BaseActionModel>
                        {
                            new MoveActionModel {Target = "{This}"}
                        }
                    }
                }
            };

            var boss = new Boss(bossModel, EngineBase);
            boss.Activate();
            boss.Show();

            boss.ActiveSkill.SetValue(skillId);
        }

        [Test]
        public void InternalPropertyLookup()
        {
            var skillId = "Skill_" + Guid.NewGuid();
            var bossModel = new BossModel
            {
                Type = "TestBoss",
                AssetId = "TestBoss",
                Skills = new List<SkillModel>
                {
                    new SkillModel
                    {
                        Id = skillId,
                        Actions = new List<BaseActionModel>
                        {
                            new SetterActionModel {Target = "{This.Health}", Value = "500"}
                        }
                    }
                }
            };

            var boss = new Boss(bossModel, EngineBase);
            boss.Activate();
            boss.Show();

            boss.ActiveSkill.SetValue(skillId);

            Assert.AreEqual(500, boss.Health.GetValue());
        }

        [Test]
        public void PlayerLookup()
        {
            var sceneId = "Scene_" + Guid.NewGuid();
            var playerId = "Player_" + Guid.NewGuid();

            var sceneModel = new SceneModel
            {
                Id = sceneId,
                AssetId = "TestScene",
                ElementsSerialized = new List<ElementModel>
                {
                    new PlayerModel
                    {
                        Id = playerId,
                        AssetId = "TestPlayer"
                    }
                }
            };

            var scene = new Scene(sceneModel, EngineBase);
            scene.Activate();
            scene.Show();

            var bindingPath = string.Format("{0}.{1}.{2}", "Root", sceneId, playerId);

            var player = EngineBase.PropertyLookup.GetProperty("{" + bindingPath + "}");

            Assert.NotNull(player);
        }
    }
}
