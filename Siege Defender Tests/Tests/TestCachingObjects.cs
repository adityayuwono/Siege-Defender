using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Models;
using SiegeDefenderTests.Stubs;

namespace SiegeDefenderTests.Tests
{
    [TestFixture]
    public class TestCachingObjects
    {
        [Test]
        public void Test1()
        {
            var sceneModel = new SceneModel
            {
                Id="TestScene",
                AssetId = "SceneMock",
                Elements = new List<ElementModel>
                {
                    new EnemyManagerModel {AssetId = "EnemyManagerMock"}
                }
            };

            var newEngine = new EngineBaseStub(new EngineModel
            {
                Objects = new List<ObjectModel>
                {
                    new ObjectModel{Id = "TestObject", AssetId = "TestObjectMock"}
                },
                Scenes = new List<SceneModel>
                {
                    sceneModel
                },
                Levels = new List<LevelModel>
                {
                    new LevelModel
                    {
                        Id = "Level1",
                        CacheList = new List<SpawnModel>
                        {
                            new SpawnModel {EnemyId = "TestObject"}
                        },
                        SpawnSequence = new List<SpawnModel>
                        {
                            new SpawnModel {EnemyId = "TestObject"}
                        }
                    }
                }
            });
            newEngine.MapInjections();
            newEngine.Activate();
            
            var activeScene = newEngine.ChangeScene(sceneModel.Id, "Level1");

            Assert.AreEqual("Level1", activeScene.EnemyManager.Level.GetValue());
        }
    }
}
