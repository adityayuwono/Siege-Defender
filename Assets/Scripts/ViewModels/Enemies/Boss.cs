using System.Collections.Generic;
using System.Linq;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models.Enemies;

namespace Scripts.ViewModels.Enemies
{
    public class Boss : EnemyBase
    {
        private readonly BossModel _model;
        public Boss(BossModel model, Object parent) : base(model, parent)
        {
            _model = model;

            foreach (var limbModel in _model.Limbs)
                _limbs.Add(new Limb(limbModel, this));

            foreach (var phaseModel in _model.Phases)
                _phases.Add(new Phase(phaseModel, this));

            foreach (var skillModel in _model.Skills)
                _skills.Add(skillModel.Id, new Skill(skillModel, this));

            ActiveSkill = new AdjustableProperty<string>("ActiveSkill", this);
            ActiveSkill.OnChange += ActivateSkill;
        }

        #region Skill
        private readonly Dictionary<string, Skill> _skills = new Dictionary<string, Skill>(); 
        public readonly AdjustableProperty<string> ActiveSkill;
        private void ActivateSkill()
        {
            var skillIdToActivate = ActiveSkill.GetValue();
            if (string.IsNullOrEmpty(skillIdToActivate)) return;// Id is empty, meaning we have just finished activating a skill
            
            if (!_skills.ContainsKey(skillIdToActivate))
            {
                var skillIds = _skills.Aggregate("", (current, skill) => current + (skill.Key + ", "));
                throw new EngineException(this, 
                    string.Format("Failed to find skill with Id: {0} on Boss: {1}.\nAvailable Skills are: {2}", skillIdToActivate, Id, skillIds));
            }
            
            var skillToActivate = _skills[skillIdToActivate];
            skillToActivate.OnSkillActivationFinished += Skill_OnActivationFinished;
            skillToActivate.Activate();
        }

        private void Skill_OnActivationFinished(Skill skill)
        {
            skill.OnSkillActivationFinished -= Skill_OnActivationFinished;
            ActiveSkill.SetValue("");
        }

        #endregion


        private readonly List<Limb> _limbs = new List<Limb>();
        private readonly List<Phase> _phases = new List<Phase>();

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var limb in _limbs)
                limb.Activate();

            foreach (var phase in _phases)
                phase.Activate();
        }

        public override void Show()
        {
            base.Show();

            foreach (var limb in _limbs)
                limb.Show();
        }


        public override float Speed
        {
            get { return 0; }
        }
    }
}
