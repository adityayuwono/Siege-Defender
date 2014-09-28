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

        public Action OnActivationFinished;
        // Activate Async, incase an action needs to wait
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

            yield break;
        }
    }
}
