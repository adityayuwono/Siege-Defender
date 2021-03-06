using Scripts.Core;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
	public class BaseCondition : TargetProperty
	{
		public readonly Property<bool> IsMatch = new Property<bool>("IsMatch");

		protected BaseCondition(BaseConditionModel model, Base parent) : base(model, parent)
		{
		}

		private Property TargetProperty
		{
			get { return Target as Property; }
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			TargetProperty.OnChange += Target_OnChanged;
			Target_OnChanged();
		}

		protected override void OnDeactivate()
		{
			TargetProperty.OnChange -= Target_OnChanged;

			base.OnDeactivate();
		}

		protected virtual void Target_OnChanged()
		{
		}
	}
}