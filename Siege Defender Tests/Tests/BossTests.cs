using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Models;
using Scripts.Models.Actions;
using Scripts.Models.Enemies;
using Scripts.ViewModels;
using Scripts.ViewModels.Enemies;
using SiegeDefenderTests.Stubs;

namespace SiegeDefenderTests.Tests
{
    [TestFixture]
    public class BossTests
    {
        [Test]
        public void Deactivating_SkillIsActive_ActiveSkillIsEmpty()
        {
            var bossModel = new BossModel
            {
                AssetId = "TestBoss",
                Skills = new List<SkillModel>
                {
                    new SkillModel
                    {
                        Id="TestSkill",
                        Actions = new List<BaseActionModel>
                        {
                            new SetterActionModel()
                        }
                    }
                }
            };

            var engineStub = new EngineBaseStub(new EngineModel());
            engineStub.MapInjections();
            engineStub.Activate();
            engineStub.Show();

            var parent = new Object(new ObjectModel { AssetId = "TestObject" }, engineStub);
            var boss = new Boss(bossModel, parent);

            boss.Activate();
            boss.Show();

            boss.ActiveSkill.SetValue("TestSkill");

            boss.Deactivate("");
            boss.Hide("");

            boss.Activate();
            boss.Show();
            boss.Deactivate("");
            boss.Hide("");

            Assert.IsEmpty(boss.ActiveSkill.GetValue());
        }
    }
}
