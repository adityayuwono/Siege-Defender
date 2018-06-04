using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models.Enemies;
using Scripts.ViewModels.Weapons;
using UnityEngine;

namespace Scripts.ViewModels.Enemies
{
    public class Boss : Enemy
    {
	    public event Action OnInterrupted;
	    public event Action OnInterrupt;

        private readonly BossModel _model;
	    private readonly List<Limb> _limbs = new List<Limb>();
	    private readonly Dictionary<string, Skill> _skills = new Dictionary<string, Skill>();
	    public readonly AdjustableProperty<string> ActiveSkill;

	    private float _skillInterruptThreshold;
	    private Skill _currentSkill;
	    private string _queuedSkillId;

        public Boss(BossModel model, Base parent) : base(model, parent)
        {
            _model = model;

	        foreach (var limbModel in _model.Limbs)
	        {
		        limbModel.Type = string.Format("{0}_{1}", _model.Type, limbModel.Id);
	        }

	        foreach (var limbModel in _model.Limbs)
	        {
		        _limbs.Add(new Limb(limbModel, this));
	        }

            foreach (var skillModel in _model.Skills)
            {
	            if (string.IsNullOrEmpty(skillModel.Id))
	            {
		            throw new EngineException(this, "Skills need Id, please provide a unique Id");
	            }
                _skills.Add(skillModel.Id, new Skill(skillModel, this));
            }

            ActiveSkill = new AdjustableProperty<string>("ActiveSkill", this);
            ActiveSkill.OnChange += ActivateSkill;
        }

        #region Skill

        /// <summary>
        /// Only one skill may be active at one time
        /// </summary>
        private void ActivateSkill()
        {
            // Only activate skill if Boss is still alive
            if (IsDead)
            {
                ActiveSkill.SetValue("");
                return;
            }

            var skillIdToActivate = ActiveSkill.GetValue();
            var skillSplit = skillIdToActivate.Split('|');// Check for Skill Queue definition, only support 1 at queue for now
            skillIdToActivate = skillSplit[0];// The first value defined is the one we want active now
            
            if (string.IsNullOrEmpty(skillIdToActivate))
                return;// Id is empty, meaning we have just finished activating a skill
            
            // Display all existing skills when failing to find a skill
            if (!_skills.ContainsKey(skillIdToActivate))
            {
                var skillIds = _skills.Aggregate("", (current, skill) => current + (skill.Key + ", "));
                throw new EngineException(this, 
                    string.Format("Failed to find skill with Id: {0}.\nAvailable Skills are: {1}", skillIdToActivate, skillIds));
            }
            
            // Get the skill that we want
            var skillToActivate = _skills[skillIdToActivate];
            if (skillToActivate.IsInterrupt)
            {
	            if (OnInterrupted != null)
	            {
		            OnInterrupted();
	            }
                // Interrupt Skill is active
                // Cancel everything that is currently happening, and Activate this skill
                if (_currentSkill != null)
                {
                    _currentSkill.Interrupt();
                    _currentSkill = null;
                }
            }
            else
            {
                // A Skill is currently active, we queue, only queue 1 skill at one time, for now...
                if (_currentSkill != null)
                {
                    // Only put one on queue at one time, fitting for interrupt mechanism... i think
	                if (skillToActivate.IsQueuedable && string.IsNullOrEmpty(_queuedSkillId))
	                {
		                _queuedSkillId = skillIdToActivate;
	                }
                    return;
                }
            }

            // Only update the queued skill when we are sure that the main skill is actually activated
	        if (skillSplit.Length == 2)
	        {
		        _queuedSkillId = skillSplit[1];
	        }

            // Activate the lucky skill
            _currentSkill = skillToActivate;
            skillToActivate.ActivationFinished += Skill_OnActivationFinished;
            _skillInterruptThreshold = skillToActivate.InterruptThreshold;
            skillToActivate.Activate();
        }

        private void Skill_OnActivationFinished(Skill skill)
        {
            _currentSkill = null;
            skill.ActivationFinished -= Skill_OnActivationFinished;
            ActiveSkill.SetValue("");// Set the active skill back to empty, to make sure the next one's OnChange is invoked

            // Activate skill in queue, if any
            if (!string.IsNullOrEmpty(_queuedSkillId))
            {
                var queuedSkillId = _queuedSkillId;// Cache the skill id, we need to clear it before calling the skill, otherwise it's going to loop
                _queuedSkillId = "";// Reset the queued skill id parameter first to avoid infinite loop
                ActiveSkill.SetValue(queuedSkillId);
            }
        }

        #endregion

        public override bool ApplyDamage(float damage, bool isCrit, Vector3 contactPoint, ProjectileBase source = null)
        {
            if (_skillInterruptThreshold > 0)
            {
                _skillInterruptThreshold -= damage;

                if (_skillInterruptThreshold <= 0)
                {
                    if (_currentSkill != null)
                    {
                        // Cache the interrupt events
                        // This is need to be done before: _currentSkill.Interrupt(false)
                        var interruptEvents = OnInterrupt;
                        
                        if (_currentSkill.Interrupt(false))
                        {
                            _currentSkill = null;

	                        if (interruptEvents != null)
	                        {
		                        interruptEvents();
	                        }
                        }
                    }
                }
            }

            return base.ApplyDamage(damage, isCrit, contactPoint, source);
        }

		protected override void OnActivate()
        {
            base.OnActivate();

	        foreach (var limb in _limbs)
	        {
		        limb.Activate();
	        }
        }

        public override void Show()
        {
            base.Show();

	        foreach (var limb in _limbs)
	        {
		        limb.Show();
	        }
        }

        public override void Hide(string reason)
        {
	        foreach (var limb in _limbs)
	        {
		        limb.Hide("Boss is hidden");
	        }

            base.Hide(reason);
        }

        protected override void OnKilled()
        {
            base.OnKilled();

	        foreach (var limb in _limbs)
	        {
		        limb.Kill();
	        }
        }

        protected override void OnDeactivate()
        {
	        if (_currentSkill != null)
	        {
		        _currentSkill.Deactivate(string.Format("Boss is deactivated, so we deactivate '{0}'", _currentSkill.Id));
	        }

            base.OnDeactivate();

            // Cleanup when deactivating
            ActiveSkill.SetValue("");
	        if (_currentSkill != null)
	        {
		        _currentSkill.ActivationFinished -= Skill_OnActivationFinished;
	        }
            _currentSkill = null;
            _queuedSkillId = "";
        }


        public override float Speed
        {
            get { return 0; }
        }

        public float BossSpeed
        {
            get { return _model.Speed; }
        }

        public event Action<Object, float> OnMoveStart;
        public event Action OnMovementFinished;

        public void Move(string moveTarget, float speedMultiplier)
        {
            if (OnMoveStart != null)
            {
                Object targetObject = null;
	            if (!string.IsNullOrEmpty(moveTarget))
	            {
		            targetObject = Root.GetViewModelAsType<Object>(moveTarget);
	            }

                OnMoveStart(targetObject, speedMultiplier);
            }
        }

        public void FinishedMovement()
        {
	        if (OnMovementFinished != null)
	        {
		        OnMovementFinished();
	        }
        }

        public override void TriggerIgnoreDelays()
        {
            base.TriggerIgnoreDelays();

            foreach (var limb in _limbs)
            {
                limb.TriggerIgnoreDelays();
            }
        }
    }
}
