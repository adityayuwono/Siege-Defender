using System;
using System.Collections.Generic;
using NUnit.Framework;
using Scripts.Models.Actions;
using Scripts.Models.Enemies;
using Scripts.ViewModels.Enemies;

namespace SiegeDefenderTests.Tests
{
    [TestFixture]
    public class BossTests : EngineBaseStub
    {
        [Test]
        [Category("Unit Tests")]
        public void Deactivating_SkillIsActive_ActiveSkillIsEmpty()
        {
            var bossModel = new BossModel
            {
                Type="TestBoss",
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

            var boss = new Boss(bossModel, EngineBase);

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

        [Test]
        public void InterruptableSkill()
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
                            new SetterActionModel {IsInterruptable = true}
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
        public void InterruptSkill_Interrupting()
        {
            var skillId = "Skill_" + Guid.NewGuid();
            var interruptSkillId = "InterruptSkill_" + Guid.NewGuid();

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
                            new SetterActionModel {Wait=5}
                        }
                    },
                    new SkillModel
                    {
                        Id = interruptSkillId,
                        IsInterrupt = true,
                        Actions = new List<BaseActionModel>
                        {
                            new SetterActionModel()
                        }
                    }
                }
            };

            var boss = new Boss(bossModel, EngineBase);
            boss.Activate();
            boss.Show();

            boss.ActiveSkill.SetValue(skillId);
            EngineBase.IntervalRunner.UpdateTime(2);
            boss.ActiveSkill.SetValue(interruptSkillId);
            
            Assert.AreEqual(interruptSkillId, boss.ActiveSkill.GetValue());
        }

        [Test]
        [Description("This is a bug, we can't reactivate an interrupted skill")]
        public void ReactivatingInterruptedSkill()
        {
            var bossId = "Boss_" + Guid.NewGuid();
            var skillId = "Skill_" + Guid.NewGuid();
            var interruptSkillId = "InterruptSkill_" + Guid.NewGuid();

            var bossModel = new BossModel
            {
                Id = bossId,
                Type = "TestBoss",
                AssetId = "TestBoss",
                Skills = new List<SkillModel>
                {
                    new SkillModel
                    {
                        Id = skillId,
                        Actions = new List<BaseActionModel>
                        {
                            new SetterActionModel {Wait=5}
                        }
                    },
                    new SkillModel
                    {
                        Id = interruptSkillId,
                        IsInterrupt = true,
                        Actions = new List<BaseActionModel>
                        {
                            new SetterActionModel()
                        }
                    }
                }
            };

            var boss = new Boss(bossModel, EngineBase);
            boss.Activate();
            boss.Show();

            boss.ActiveSkill.SetValue(skillId);
            EngineBase.IntervalRunner.UpdateTime(1);
            boss.ActiveSkill.SetValue(interruptSkillId);
            EngineBase.IntervalRunner.UpdateTime(1);
            boss.ActiveSkill.SetValue(skillId);

            Assert.AreEqual(skillId, boss.ActiveSkill.GetValue());
        }
    }
}
