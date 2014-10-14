using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Models.Actions;
using UnityEngine;

namespace Scripts.ViewModels.Actions
{
    /// <summary>
    /// Collection of actions, managed it's own activation and deactivation
    /// </summary>
    public class ActionCollection : List<BaseAction>
    {
        private readonly List<BaseActionModel> _models;
        private readonly Base _parent;
        public ActionCollection(List<BaseActionModel> models, Base parent)
        {
            _models = models;
            _parent = parent;

            foreach (var actionModel in models)
            {
                // Get new instance of ActionVM
                var actionVM = parent.Root.IoCContainer.GetInstance<BaseAction>(actionModel.GetType(), new object[] { actionModel, _parent });
                Add(actionVM);
            }
        }

        public void Activate()
        {
            if (_isActionInvoking)
                return;

            _isActionInvoking = true;
            _isInterrupted = false;
            ActivateActions(0);
        }

        /// <summary>
        /// Activate actions in this collection starting from startIndex
        /// </summary>
        /// <param name="startIndex">The index of action we want to start with</param>
        private void ActivateActions(int startIndex)
        {
            _parent.Root.StartCoroutine(ActivateActionAsync(startIndex));
        }

        /// <summary>
        /// Invoked by the Enumerator when the action sequence has finished Activated
        /// </summary>
        public Action OnActivationFinished;
        /// <summary>
        /// Activate the Actions in sequence and async, if an action have a wait duration defined, it will wait for that duration before proceeding with the next action
        /// </summary>
        private IEnumerator ActivateActionAsync(int startIndex)
        {
            for (var i = startIndex; i < Count; i++)
            {
                var action = this[i];

                if (_isInterrupted)
                    yield break;

                if (action is MoveAction)
                {
                    // Move action have special treatment, we cannot be sure when it will finish
                    var moveAction = (MoveAction) action;
                    // So we need to wait for the finish event to be invoked from the target
                    moveAction.OnActionFinished += () =>
                    {
                        // If it is a move action we wait until the finish event is invoked, then continue the routine
                        moveAction.OnActionFinished = null;
                        // Call the next index
                        ActivateActions(i + 1);
                    };
                    // Invoke the move action
                    moveAction.Invoke();
                    // Break this routine, we no longer need this, as we already queue a new one after the move is finished
                    yield break;
                }
                action.Invoke();
                // Wait before invoking the next action
                yield return new WaitForSeconds(action.Wait);
            }

            DeactivateActions();
            
            if (OnActivationFinished != null)
                OnActivationFinished();
        }

        private bool _isInterrupted;
        private bool _isActionInvoking;

        public void Interrupt()
        {
            // Mark as interrupted to stop the very next action from being invoked 
            _isInterrupted = true;
            OnActivationFinished = null;
            // Deactivate everything immediately
            DeactivateActions();
        }

        public void Deactivate()
        {
            Interrupt();
        }

        private void DeactivateActions()
        {
            // Deactivate only activated actions, this is way easier than checking the index, and have similar result
            foreach (var action in this.Where(action => action.IsActive))
                action.Deactivate("Done Invoking actions");

            _isActionInvoking = false;
        }
    }
}
