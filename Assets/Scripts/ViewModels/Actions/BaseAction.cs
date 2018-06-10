using System;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
	public class BaseAction : TargetProperty
	{
		public Action OnActionFinished;

		private readonly BaseActionModel _model;

		public BaseAction(BaseActionModel model, Base parent) : base(model, parent)
		{
			_model = model;
		}

		public override string Id
		{
			get { return base.Id + ":" + _model.Target + ":" + _model.Value; }
		}

		public float Wait
		{
			get { return _model.Wait; }
		}

		public bool IsInterruptable
		{
			get { return _model.IsInterruptable; }
		}

		public bool IsActive { get; private set; }

		public virtual void Invoke()
		{
			IsActive = true;
			Activate();
		}

		protected override void OnDeactivate()
		{
			base.OnDeactivate();
			IsActive = false;
		}
	}
}