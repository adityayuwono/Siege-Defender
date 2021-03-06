using System.Collections.Generic;
using Scripts.Models.Actions;
using Scripts.ViewModels.Actions;

namespace Scripts.ViewModels.Enemies
{
	public class Triggered : Base
	{
		private readonly ActionCollection _actions;

		private readonly List<BaseCondition> _conditions = new List<BaseCondition>();
		private readonly TriggeredModel _model;

		public Triggered(TriggeredModel model, Base parent) : base(model, parent)
		{
			_model = model;

			if (_model.Conditions != null)
			{
				foreach (var conditionModel in _model.Conditions)
				{
					// Get new instance of ActionVM
					var conditionViewModel =
						IoC.IoCContainer.GetInstance<BaseCondition>(conditionModel.GetType(), new object[] {conditionModel, this});
					_conditions.Add(conditionViewModel);
				}
			}

			_actions = new ActionCollection(_model.Actions, this);
		}

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
			{
				isMatch &= condition.IsMatch.GetValue();
			}

			if (isMatch)
			{
				if (_model.TriggerOnce)
				{
					foreach (var condition in _conditions)
					{
						condition.IsMatch.OnChange -= Conditions_OnChange;
					}
				}

				_actions.Activate();
			}
		}
	}
}