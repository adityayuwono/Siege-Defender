using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class BaseView : IBase
    {
        private readonly Base _viewModel;
	    private bool _isShown = false;

        protected BaseView(Base viewModel, BaseView parent)
        {
            _viewModel = viewModel;

            _viewModel.OnShow += OnShow;
            _viewModel.OnHide += OnHide;

            _viewModel.OnDestroy += OnDestroy;
        }

		public string Id
        {
            get { return _viewModel.Id; }
        }

        public string FullId
        {
            get { return _viewModel.FullId; }
        }

	    protected virtual void OnShow()
	    {
		    if (_isShown)
		    {
			    throw new EngineException(this, "Trying to show twice");
		    }

			Debug.LogWarning(string.Format("Showing {0} for the first time", Id));

		    _isShown = true;
	    }

	    protected virtual void OnHide(string reason)
	    {
		    if (!_isShown)
		    {
			    throw new EngineException(this, "Trying to hide twice");
		    }

			Debug.LogWarning(string.Format("Hiding {0} the first time", Id));

		    _isShown = false;
	    }

        protected virtual void OnDestroy()
        {
            _viewModel.OnShow -= OnShow;
            _viewModel.OnHide -= OnHide;
            _viewModel.OnDestroy -= OnDestroy;
        }
    }
}
