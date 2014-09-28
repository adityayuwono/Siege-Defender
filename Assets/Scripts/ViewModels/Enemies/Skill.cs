using System;
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

        protected override void OnActivate()
        {
            base.OnActivate();

            _actions.OnActivationFinished += OnActivationFinished;
            _actions.Activate();
        }

        private void OnActivationFinished()
        {
            _actions.OnActivationFinished -= OnActivationFinished;
            Deactivate("Done activating Skill");
        }
    }
}
