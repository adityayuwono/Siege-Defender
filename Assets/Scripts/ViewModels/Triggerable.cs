using System.Collections.Generic;
using Scripts.Models;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels
{
	public class Triggerable : Base
	{
		private readonly TriggerableModel _model;
		private readonly List<Triggered> _triggers = new List<Triggered>();

		protected Triggerable(TriggerableModel model, Base parent) : base(model, parent)
		{
			_model = model;

			if (_model.Triggers != null)
			{
				foreach (var triggeredModel in _model.Triggers)
				{
					var triggered =
						IoC.IoCContainer.GetInstance<Triggered>(triggeredModel.GetType(), new object[] {triggeredModel, this});
					_triggers.Add(triggered);
				}
			}
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			foreach (var triggered in _triggers)
			{
				triggered.Activate();
			}
		}

		protected override void OnDeactivate()
		{
			foreach (var triggered in _triggers)
			{
				triggered.Deactivate("Triggerable is deactivated");
			}

			base.OnDeactivate();
		}
	}
}