using System;
using Scripts.Models.Enemies;
using Scripts.ViewModels.Actions;

namespace Scripts.ViewModels.Enemies
{
	public class Skill : Triggerable
	{
		private readonly ActionCollection _actions;
		private readonly SkillModel _model;

		public Skill(SkillModel model, Base parent) : base(model, parent)
		{
			_model = model;

			_actions = new ActionCollection(_model.Actions, this);
		}

		public bool IsQueuedable
		{
			get { return _model.IsQueuedable; }
		}

		public bool IsInterrupt
		{
			get { return _model.IsInterrupt; }
		}

		public float InterruptThreshold
		{
			get { return _model.InterruptThreshold; }
		}

		public event Action<Skill> ActivationFinished;

		protected override void OnActivate()
		{
			base.OnActivate();

			_actions.OnActivationFinished += Action_OnActivationFinished;
			_actions.Activate();
		}

		private void Action_OnActivationFinished()
		{
			Deactivate("Done activating Skill");

			OnActivationFinished();
		}

		private void OnActivationFinished()
		{
			if (ActivationFinished != null)
				ActivationFinished(this);
		}

		protected override void OnDeactivate()
		{
			_actions.OnActivationFinished -= Action_OnActivationFinished;
			_actions.Deactivate();

			base.OnDeactivate();
		}

		public bool Interrupt(bool absolute = true)
		{
			var isInterruptSuccessful = _actions.Interrupt(absolute);
			if (isInterruptSuccessful)
			{
				ActivationFinished = null;
				Deactivate("Skill is interrupted"); // Deactivate directly, and avoid any further activation
			}

			return isInterruptSuccessful;
		}
	}
}