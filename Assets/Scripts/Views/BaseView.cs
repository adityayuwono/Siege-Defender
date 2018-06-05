using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.ViewModels;

namespace Scripts.Views
{
	public class BaseView : IBase
	{
		private readonly Base _viewModel;
		private bool _isShown;

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
			if (_isShown) throw new EngineException(this, "Trying to show twice");

			_isShown = true;
		}

		protected virtual void OnHide(string reason)
		{
			if (!_isShown) throw new EngineException(this, "Trying to hide twice");

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