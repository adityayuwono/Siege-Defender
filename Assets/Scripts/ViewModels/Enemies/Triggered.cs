using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.Models.Actions;
using Scripts.ViewModels.Actions;

namespace Scripts.ViewModels.Enemies
{
    public class Triggered : Base
    {
        private readonly TriggeredModel _model;
        public Triggered(TriggeredModel model, Base parent) : base(model, parent)
        {
            _model = model;

            foreach (var conditionModel in _model.Conditions)
            {
                // Get new instance of ActionVM
                var conditionViewModel = Root.IoCContainer.GetInstance<BaseCondition>(conditionModel.GetType(), new object[] { conditionModel, this });
                _conditions.Add(conditionViewModel);
            }

            _actions = new ActionCollection(_model.Actions, this);
        }

        private readonly List<BaseCondition> _conditions = new List<BaseCondition>();
        private readonly ActionCollection _actions;

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var condition in _conditions)
            {
                condition.Activate();
                condition.IsMatch.OnChange += Conditions_OnChange;
            }

            Conditions_OnChange();
        }
        
        protected override void OnDeactivate()
        {
            foreach (var condition in _conditions)
            {
                condition.IsMatch.OnChange -= Conditions_OnChange;
                condition.Deactivate(string.Format("Triggered on ({0}) is deactivated", FullId));
            }

            _actions.Deactivate();

            base.OnDeactivate();
        }

        private void Conditions_OnChange()
        {
            var isMatch = true;
            foreach (var condition in _conditions)
                isMatch &= condition.IsMatch.GetValue();

            if (isMatch)
            {
                if (_model.TriggerOnce)
                    foreach (var condition in _conditions)
                        condition.IsMatch.OnChange -= Conditions_OnChange;
                
                _actions.Activate();
            }
        }
    }

    public class EventTriggered : Triggered
    {
        private readonly EventTriggeredModel _model;
        public EventTriggered(EventTriggeredModel model, Base parent) : base(model, parent)
        {
            _model = model;

            if (_model.Event == Event.None)
                throw new EngineException(this, "EventTrigger doesn't have an event specified");
        }

        private Object _parentObject;
        protected override void OnActivate()
        {
            _parentObject = GetParent<Object>();
            if (_parentObject == null)
                throw new EngineException(this, "Failed to find Parent Object");

            switch (_model.Event)
            {
                case Event.Interrupt:
                {
                    var parentBoss = _parentObject as Boss;
                    if (parentBoss != null)
                        parentBoss.OnInterrupt += InvokeEvent;
                }
                    break;
                case Event.Click:
                {
                    var parentButton = _parentObject as Button;
                    if (parentButton != null)
                        parentButton.OnClick += InvokeEvent;
                }
                    break;
                case Event.Break:
                {
                    var parentButton = _parentObject as Limb;
                    if (parentButton != null)
                        parentButton.OnBreak += InvokeEvent;
                }
                    break;
                case Event.Spawn:
                {
                    var parentButton = _parentObject as EnemyBase;
                    if (parentButton != null)
                        parentButton.OnSpawn += InvokeEvent;
                }
                    break;
                case Event.Attack:
                {
                    var parentButton = _parentObject as EnemyBase;
                    if (parentButton != null)
                        parentButton.OnAttack += InvokeEvent;
                }
                    break;
                case Event.GameOver:
                {
                    var parentButton = _parentObject as Player;
                    if (parentButton != null)
                        parentButton.OnGameOver += InvokeEvent;
                }
                    break;
            }
        }

        private void InvokeEvent()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate()
        {
            switch (_model.Event)
            {
                case Event.Interrupt:
                {
                    var parentBoss = _parentObject as Boss;
                    if (parentBoss != null)
                        parentBoss.OnInterrupt -= InvokeEvent;
                }
                    break;
                case Event.Click:
                {
                    var parentButton = _parentObject as Button;
                    if (parentButton != null)
                        parentButton.OnClick -= InvokeEvent;
                }
                    break;
                case Event.Break:
                {
                    var parentButton = _parentObject as Limb;
                    if (parentButton != null)
                        parentButton.OnBreak -= InvokeEvent;
                }
                    break;
                case Event.Spawn:
                {
                    var parentButton = _parentObject as EnemyBase;
                    if (parentButton != null)
                        parentButton.OnSpawn -= InvokeEvent;
                }
                    break;
                case Event.Attack:
                {
                    var parentButton = _parentObject as EnemyBase;
                    if (parentButton != null)
                        parentButton.OnAttack -= InvokeEvent;
                }
                    break;
                case Event.GameOver:
                    {
                        var parentButton = _parentObject as Player;
                        if (parentButton != null)
                            parentButton.OnGameOver -= InvokeEvent;
                    }
                    break;
            }
        }
    }
}
