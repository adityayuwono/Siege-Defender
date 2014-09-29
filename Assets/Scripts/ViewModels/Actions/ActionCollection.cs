using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Models.Actions;
using UnityEngine;

namespace Scripts.ViewModels.Actions
{
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
            _parent.Root.StartCoroutine(ActivateActionAsync());
        }

        /// <summary>
        /// Invoked by the Enumerator when the action sequence has finished Activated
        /// </summary>
        public Action OnActivationFinished;
        /// <summary>
        /// Activate the Actions in sequence and async, if an action have a wait duration defined, it will for that duration before proceeding with the next action
        /// </summary>
        /// <returns></returns>
        private IEnumerator ActivateActionAsync()
        {
            for (var i = 0; i < Count; i++)
            {
                var action = this[i];
                if (action.WaitDuration > 0.1f)
                {
                    action.Invoke();
                    yield return new WaitForSeconds(action.WaitDuration);
                }
                else
                    action.Invoke();
            }

            foreach (var action in this)
                action.Deactivate("Done Invoking actions");

            if (OnActivationFinished != null)
                OnActivationFinished();
        }
    }
}
