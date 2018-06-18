using Scripts.Models.Actions;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels.Actions
{
	public class MoveAction : BaseAction
	{
		private readonly MoveActionModel _model;

		public MoveAction(MoveActionModel model, Base parent) : base(model, parent)
		{
			_model = model;
		}

		public override void Invoke()
		{
			base.Invoke();
			var target = GetParent<Boss>();
			if (target != null)
			{
				target.OnMovementFinished += OnMovementFinished;
				target.Move(_model.MoveTarget, _model.SpeedMultiplier);
			}
		}

		private void OnMovementFinished()
		{
			if (OnActionFinished != null)
			{
				OnActionFinished();
			}
		}
	}
}