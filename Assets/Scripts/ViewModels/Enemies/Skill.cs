﻿using System;
using Scripts.Models.Enemies;
using Scripts.ViewModels.Actions;

namespace Scripts.ViewModels.Enemies
{
    public class Skill : Base
    {
        private readonly SkillModel _model;
 
        public Skill(SkillModel model, Base parent) : base(model, parent)
        {
            _model = model;

            _actions = new ActionCollection(_model.Actions, this);
        }

        private readonly ActionCollection _actions;

        public Action<Skill> OnSkillActivationFinished;

        public bool IsQueuedable
        {
            get { return _model.IsQueuedable; }
        }

        public bool IsInterrupt
        {
            get { return _model.IsInterrupt; }
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            _actions.OnActivationFinished += Action_OnActivationFinished;
            _actions.Activate();
        }

        private void Action_OnActivationFinished()
        {
            _actions.OnActivationFinished -= Action_OnActivationFinished;
            Deactivate("Done activating Skill");

            if (OnSkillActivationFinished != null)
                OnSkillActivationFinished(this);
        }

        public void Interrupt()
        {
            _actions.Interrupt();
            OnSkillActivationFinished = null;
            Action_OnActivationFinished();
        }
    }
}
