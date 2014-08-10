using System.Collections;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class BaseController : MonoBehaviour
    {
        protected ObjectViewModel ViewModel { get; private set; }

        private bool _isSet;
        public void Setup(ObjectViewModel viewModel)
        {
            _isSet = true;
            ViewModel = viewModel;

            OnSetup();
        }
        protected virtual void OnSetup() { }
        private void Start()
        {
            if (!_isSet)
                Debug.LogError(string.Format("{0} for {1} is not Set", GetType(), name));
        }



        protected bool IsDead { get; private set; }
        public void Kill()
        {
            if (IsDead) return;

            IsDead = true;
            OnKilled();
        }
        protected virtual void OnKilled() { }
        protected IEnumerator DelayedDeath(float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }

        public virtual void ClearEvents() { }
    }
}
