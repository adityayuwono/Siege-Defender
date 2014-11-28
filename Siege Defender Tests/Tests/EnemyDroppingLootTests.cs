using System;
using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Models;
using Scripts.Models.Actions;
using Scripts.Models.Enemies;
using Scripts.ViewModels;
using Scripts.ViewModels.Enemies;
using UnityEngine;
using Event = Scripts.Models.Actions.Event;

namespace SiegeDefenderTests.Tests
{
    [TestFixture]
    public class EnemyDroppingLootTests : EngineBaseStub
    {
        [Test]
        public void DropItemWhenDead()
        {
            var lootTableId = "LootTable_" + Guid.NewGuid();
            var inventoryId = "Inventory_" + Guid.NewGuid();
            var itemId = "Item_" + Guid.NewGuid();

            var playerSettingsModel = new PlayerSettingsModel
            {
                Inventories = new List<InventoryModel>
                {
                    new InventoryModel {Id = inventoryId}
                }
            };
            EngineBase.SetPlayerSettingsModel(playerSettingsModel);

            var inventoryModel = new InventoryModel
            {
                Id=inventoryId,
                Source = inventoryId,
                AssetId = "TestInventory"
            };

            var enemyModel = new EnemyBaseModel
            {
                AssetId = "TestEnemy",
                Type = "TestEnemy",
                TriggersSerialized = new List<TriggeredModel>
                {
                    new EventTriggeredModel
                    {
                        Event = Event.Death,
                        Actions = new List<BaseActionModel>
                        {
                            new CreateItemActionModel {Target = "{Root." + inventoryId + "}", Value = lootTableId}
                        }
                    }
                }
            };

            EngineBase.AddLootTable(new LootTable(
                new LootTableModel
                {
                    Id = lootTableId, 
                    Drops = 1,
                    Loots = new List<LootModel>
                    {
                        new LootModel{ItemId = itemId, Chance = 100}
                    }
                }, EngineBase));

            EngineBase.AddItemModel(new ItemModel{Id = itemId, AssetId = "TestItem"});

            var inventory = new Inventory(inventoryModel, EngineBase);
            inventory.Activate();
            inventory.Show();

            var enemy = new EnemyBase(enemyModel, EngineBase);
            enemy.Activate();
            enemy.Show();

            var itemCount = 0;

            inventory.OnChildrenChanged += () => itemCount++;

            enemy.ApplyDamage(2, Vector3.zero);

            Assert.AreEqual(1, itemCount);
        }

        [Test]
        public void DropItemChance_IsRandomized()
        {
            var lootTableId = "LootTable_" + Guid.NewGuid();
            var itemId1 = "Item1_" + Guid.NewGuid();
            var itemId2 = "Item2_" + Guid.NewGuid();
            var itemId3 = "Item3_" + Guid.NewGuid();

            EngineBase.AddItemModel(new ItemModel { Id = itemId1, AssetId = "TestItem" });
            EngineBase.AddItemModel(new ItemModel { Id = itemId2, AssetId = "TestItem" });
            EngineBase.AddItemModel(new ItemModel { Id = itemId3, AssetId = "TestItem" });

            EngineBase.AddLootTable(new LootTable(
                new LootTableModel
                {
                    Id = lootTableId,
                    Drops = 1,
                    Loots = new List<LootModel>
                    {
                        new LootModel {ItemId = itemId1, Chance = 25},
                        new LootModel {ItemId = itemId2, Chance = 25},
                        new LootModel {ItemId = itemId3, Chance = 25}
                    }
                }, EngineBase));

            var totalItemsDropped = 0;
            for (int i = 0; i < 1000; i++)
            {
                var items = EngineBase.GetLoot(lootTableId);
                totalItemsDropped += items.Count;
            }

            Assert.IsTrue(totalItemsDropped>500 && totalItemsDropped < 650);
        }

        [Test]
        public void LimbBreak_BossDeath_LootDrop()
        {
            var lootTableId = "LootTable_" + Guid.NewGuid();
            var inventoryId = "Inventory_" + Guid.NewGuid();
            var itemId = "Item_" + Guid.NewGuid();
            var limbId = "Limb_" + Guid.NewGuid();

            EngineBase.AddLootTable(new LootTable(
                new LootTableModel
                {
                    Id = lootTableId,
                    Drops = 1,
                    Loots = new List<LootModel>
                    {
                        new LootModel{ItemId = itemId, Chance = 100}
                    }
                }, EngineBase));

            EngineBase.AddItemModel(new ItemModel { Id = itemId, AssetId = "TestItem" });

            var playerSettingsModel = new PlayerSettingsModel
            {
                Inventories = new List<InventoryModel>
                {
                    new InventoryModel {Id = inventoryId}
                }
            };
            EngineBase.SetPlayerSettingsModel(playerSettingsModel);

            var inventoryModel = new InventoryModel
            {
                Id = inventoryId,
                Source = inventoryId,
                AssetId = "TestInventory"
            };

            var bossModel = new BossModel
            {
                Type = "TestBoss",
                AssetId = "TestBoss",
                Health = 3,
                Limbs = new List<LimbModel>
                {
                    new LimbModel
                    {
                        Id=limbId,
                        Type="TestBoss_Hand",
                        AssetId = "TestLimb",
                        TriggersSerialized = new List<TriggeredModel>
                        {
                            new EventTriggeredModel
                            {
                                Event = Event.Death,
                                Actions = new List<BaseActionModel>
                                {
                                    new CreateItemActionModel {Target = "{Root." + inventoryId + "}", Value = lootTableId}
                                }
                            }
                        }
                    }
                }
            };

            var boss = new Boss(bossModel, EngineBase);
            boss.Activate();
            boss.Show();

            var inventory = new Inventory(inventoryModel, EngineBase);
            inventory.Activate();
            inventory.Show();

            var itemCount = 0;
            inventory.OnChildrenChanged += () => itemCount++;

            var limb = EngineBase.GetViewModelAsType<Limb>(limbId);
            limb.ApplyDamage(2, Vector3.zero);

            boss.ApplyDamage(1, Vector3.zero);

            Assert.AreEqual(1, itemCount);
        }

        [Test]
        public void LimbBreak_BossIsNotDead_LootDoesntDrop()
        {
            var lootTableId = "LootTable_" + Guid.NewGuid();
            var inventoryId = "Inventory_" + Guid.NewGuid();
            var itemId = "Item_" + Guid.NewGuid();
            var limbId = "Limb_" + Guid.NewGuid();

            EngineBase.AddLootTable(new LootTable(
                new LootTableModel
                {
                    Id = lootTableId,
                    Drops = 1,
                    Loots = new List<LootModel>
                    {
                        new LootModel{ItemId = itemId, Chance = 100}
                    }
                }, EngineBase));

            EngineBase.AddItemModel(new ItemModel { Id = itemId, AssetId = "TestItem" });

            var playerSettingsModel = new PlayerSettingsModel
            {
                Inventories = new List<InventoryModel>
                {
                    new InventoryModel {Id = inventoryId}
                }
            };
            EngineBase.SetPlayerSettingsModel(playerSettingsModel);

            var inventoryModel = new InventoryModel
            {
                Id = inventoryId,
                Source = inventoryId,
                AssetId = "TestInventory"
            };

            var bossModel = new BossModel
            {
                Type = "TestBoss",
                AssetId = "TestBoss",
                Health = 3,
                Limbs = new List<LimbModel>
                {
                    new LimbModel
                    {
                        Id=limbId,
                        Type="TestBoss_Limb",
                        AssetId = "TestLimb",
                        TriggersSerialized = new List<TriggeredModel>
                        {
                            new EventTriggeredModel
                            {
                                Event = Event.Death,
                                Actions = new List<BaseActionModel>
                                {
                                    new CreateItemActionModel {Target = "{Root." + inventoryId + "}", Value = lootTableId}
                                }
                            }
                        }
                    }
                }
            };

            var boss = new Boss(bossModel, EngineBase);
            boss.Activate();
            boss.Show();

            var inventory = new Inventory(inventoryModel, EngineBase);
            inventory.Activate();
            inventory.Show();

            var itemCount = 0;
            inventory.OnChildrenChanged += () => itemCount++;

            var limb = EngineBase.GetViewModelAsType<Limb>(limbId);
            limb.ApplyDamage(2, Vector3.zero);

            Assert.AreEqual(0, itemCount);
        }
    }
}
