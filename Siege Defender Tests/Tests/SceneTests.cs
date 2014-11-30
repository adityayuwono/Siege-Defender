using System;
using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Models;
using Scripts.Models.Actions;
using Scripts.Models.GUIs;
using Scripts.ViewModels;

namespace SiegeDefenderTests.Tests
{
    public class SceneTests : EngineBaseStub
    {
        [Test]
        public void Deactivate()
        {
            var scene1Model = new SceneModel
            {
                Id = "Scene1_" + Guid.NewGuid(), 
                AssetId = "TestScene"
            };
            var scene1 = new Scene(scene1Model, EngineBase);

            var scene2Model = new SceneModel
            {
                Id = "Scene2_" + Guid.NewGuid(), 
                AssetId = "TestScene"
            };
            var scene2 = new Scene(scene2Model, EngineBase);

            scene1.Activate();
            scene1.Show();

            scene1.Hide("Changing Scene");
            scene1.Deactivate("Changing Scene");

            scene2.Activate();
            scene2.Show();
        }

        [Test]
        [Category("Unit Tests")]
        public void ChangingScene_AB()
        {
            var scene1Model = new SceneModel
            {
                Id = "Scene1_" + Guid.NewGuid(),
                AssetId = "TestScene"
            };
            var scene1 = new Scene(scene1Model, EngineBase);

            var scene2Model = new SceneModel
            {
                Id = "Scene2_" + Guid.NewGuid(),
                AssetId = "TestScene"
            };
            var scene2 = new Scene(scene2Model, EngineBase);

            EngineBase.AddScene(scene1);
            EngineBase.AddScene(scene2);

            EngineBase.ChangeScene(scene1.Id);
            EngineBase.ChangeScene(scene2.Id);
        }

        [Test]
        [Category("Integration Tests")]
        public void ChangingScene_UsingButtons_ABA()
        {
            var scene2Id = "Scene2_" + Guid.NewGuid();

            var scene1Model = new SceneModel
            {
                Id = "Scene1_" + Guid.NewGuid(),
                AssetId = "TestScene",
                ElementsSerialized = new List<ElementModel>
                {
                    new ButtonGUIModel
                    {
                        TriggersSerialized = new List<TriggeredModel>
                        {
                            new EventTriggeredModel()
                            {
                                Event = Event.Click,
                                Actions = new List<BaseActionModel>
                                {
                                    new LoadSceneActionModel{Target = scene2Id}
                                }
                            }
                        }
                    }
                }
            };
            var scene1 = new Scene(scene1Model, EngineBase);

            var scene2Model = new SceneModel
            {
                Id = scene2Id,
                AssetId = "TestScene"
            };
            var scene2 = new Scene(scene2Model, EngineBase);

            EngineBase.AddScene(scene1);
            EngineBase.AddScene(scene2);

            EngineBase.ChangeScene(scene1.Id);
            EngineBase.ChangeScene(scene2.Id);
            EngineBase.ChangeScene(scene1.Id);
        }
    }
}

