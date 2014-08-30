using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class BaseController : MonoBehaviour, IBase
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
                throw new EngineException(this, "Failed to continue, need Setup first");
        }
        
        public string Id
        {
            get { return ViewModel.Id; }
        }
    }
}
