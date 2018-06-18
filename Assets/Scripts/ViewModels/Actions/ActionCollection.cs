using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Helpers;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
	/// <summary>
	///     Collection of actions, manages it's own activation and deactivation
	/// </summary>
	public class ActionCollection : List<BaseAction>
	{
		/// <summary>
		///     Invoked by the Enumerator when the action sequence has finished Activated
		/// </summary>
		public event Action OnActivationFinished;

		private readonly Base _parent;
		private BaseAction _currentAction;

		private int _currentIndex;
		private bool _isActionInvoking;
		private bool _isInterruptable;

		public ActionCollection(List<BaseActionModel> models, Base parent)
		{
			_parent = parent;

			foreach (var actionModel in models)
			{
				// Get new instance of ActionVM
				var action = IoC.IoCContainer.GetInstance<BaseAction>(actionModel.GetType(), new object[] {actionModel, _parent});
				Add(action);
			}
		}

		public void Activate()
		{
			if (_isActionInvoking)
			{
				return;
			}

			foreach (var action in this)
			{
				if (action.IsActive)
				{
					throw new EngineException(_parent, string.Format("Failed to activate, action {0} is still active"));
				}
			}

			_isInterruptable = false;
			_isActionInvoking = true;
			_currentIndex = 0;
			ActivateActions();
		}

		public bool Interrupt(bool absolute = true)
		{
			var isInterruptable = absolute || _isInterruptable;

			if (isInterruptable)
			{
				_parent.SDRoot.IntervalRunner.UnsubscribeFromInterval(ActivateActions);
				OnActivationFinished = null;
				// Deactivate everything immediately
				DeactivateActions();
			}

			return isInterruptable;
		}

		public void Deactivate()
		{
			Interrupt();
		}

		/// <summary>
		///     Activate actions in this collection starting from startIndex
		/// </summary>
		private void ActivateActions()
		{
			// Unsubsribe the interval first
			_parent.Root.IntervalRunner.UnsubscribeFromInterval(ActivateActions);

			if (_currentIndex < Count)
			{
				_currentAction = this[_currentIndex];
				_isInterruptable = _currentAction.IsInterruptable;
				_currentIndex++;
				if (_currentAction is MoveAction)
				{
					// Move action have special treatment, we cannot be sure when it will finish
					var moveAction = (MoveAction) _currentAction;
					// So we need to wait for the finish event to be invoked from the target
					moveAction.OnActionFinished += () =>
					{
						// If it is a move action we wait until the finish event is invoked, then continue the routine
						moveAction.OnActionFinished = null;
						// Call the next index
						ActivateActions();
					};
					// Invoke the move action
					moveAction.Invoke();
				}
				else
				{
					_currentAction.Invoke();
					// Edge case where the action is Load Scene Action
					if (_currentAction != null)
					{
						_parent.Root.IntervalRunner.SubscribeToInterval(ActivateActions, _currentAction.Wait, !(_currentAction.Wait > 0));
					}
				}
			}
			else
			{
				DeactivateActions();

				if (OnActivationFinished != null)
				{
					OnActivationFinished();
				}
			}
		}

		private void DeactivateActions()
		{
			// Deactivate only activated actions, this is way easier than checking the index, and have similar result
			foreach (var action in this.Where(action => action.IsActive)) action.Deactivate("Done Invoking actions");

			if (_currentAction != null) _currentAction.OnActionFinished = null;

			_currentAction = null;
			_currentIndex = 0;

			_isActionInvoking = false;
		}
	}
}