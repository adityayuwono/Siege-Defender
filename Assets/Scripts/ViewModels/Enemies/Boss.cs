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

            foreach (var triggeredModel in _model.Triggers)
                _triggers.Add(new Triggered(triggeredModel, this));

            foreach (var skillModel in _model.Skills)
            {
                if (string.IsNullOrEmpty(skillModel.Id))
                    throw new EngineException(this, "Skills need Id, please provide a unique Id");
                _skills.Add(skillModel.Id, new Skill(skillModel, this));
            }

            ActiveSkill = new AdjustableProperty<string>("ActiveSkill", this);
            ActiveSkill.OnChange += ActivateSkill;

            MoveToARandomWaypoint = new AdjustableProperty<bool>("MoveToARandomWaypoint", this);
        }

        public readonly AdjustableProperty<bool> MoveToARandomWaypoint;

        #region Skill

        private bool _isASkillActive;
        private string QueuedSkillId;
        private readonly Dictionary<string, Skill> _skills = new Dictionary<string, Skill>(); 
        /// <summary>
        /// Only one skill may be active at one time
        /// </summary>
        public readonly AdjustableProperty<string> ActiveSkill;
        private void ActivateSkill()
        {
            var skillIdToActivate = ActiveSkill.GetValue();
            if (string.IsNullOrEmpty(skillIdToActivate)) return;// Id is empty, meaning we have just finished activating a skill

            if (!_skills.ContainsKey(skillIdToActivate))
            {
                var skillIds = _skills.Aggregate("", (current, skill) => current + (skill.Key + ", "));
                throw new EngineException(this, 
                    string.Format("Failed to find skill with Id: {0}.\nAvailable Skills are: {1}", skillIdToActivate, skillIds));
            }
            
            var skillToActivate = _skills[skillIdToActivate];
            // A Skill is currently active, we queue, only queue 1 skill at one time, for now...
            if (_isASkillActive)
            {
                if (skillToActivate.IsQueuedable)
                {
                    QueuedSkillId = skillIdToActivate;
                    UnityEngine.Debug.LogError("Queued "+skillIdToActivate);
                }
                ActiveSkill.SetValue("");
                return;
            }

            _isASkillActive = true;
            skillToActivate.OnSkillActivationFinished += Skill_OnActivationFinished;
            skillToActivate.Activate();
        }

        private void Skill_OnActivationFinished(Skill skill)
        {
            skill.OnSkillActivationFinished -= Skill_OnActivationFinished;
            ActiveSkill.SetValue("");// Set the active skill back to empty
            _isASkillActive = false;

            if (!string.IsNullOrEmpty(QueuedSkillId))
            {
                var queuedSkillId = QueuedSkillId;
                QueuedSkillId = "";
                ActiveSkill.SetValue(queuedSkillId);
            }
        }

        #endregion


        private readonly List<Limb> _limbs = new List<Limb>();
        private readonly List<Triggered> _triggers = new List<Triggered>();

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var limb in _limbs)
                limb.Activate();

            foreach (var phase in _triggers)
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

        public float BossSpeed
        {
            get { return _model.Speed; }
        }
    }
}
