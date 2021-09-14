using Scripts.Core;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.ViewModels;

namespace Scripts.Views
{
	public class BaseView : HaveRoot, IBaseView
	{
		private readonly Base _viewModel;
		private bool _isShown;

		protected BaseView(Base viewModel, BaseView parent) : 
			base(viewModel, parent)
		{
			_viewModel = viewModel;

			_viewModel.OnShow += OnShow;
			_viewModel.OnHide += OnHide;

			_viewModel.OnDestroy += OnDestroy;

			Parent = parent;
		}

		protected virtual void OnShow()
		{
			if (_isShown)
			{
				throw new EngineException(this, "Trying to show twice");
			}

			_isShown = true;
		}

		protected virtual void OnHide(string reason)
		{
			if (!_isShown)
			{
				throw new EngineException(this, "Trying to hide twice");
			}

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