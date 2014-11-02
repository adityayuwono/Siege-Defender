using Scripts.Interfaces;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class BaseView : IBase
    {
        private readonly Base _viewModel;

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

        protected virtual void OnShow() { }
        protected virtual void OnHide(string reason){ }
        protected virtual void OnDestroy()
        {
            _viewModel.OnShow -= OnShow;
            _viewModel.OnHide -= OnHide;
            _viewModel.OnDestroy -= OnDestroy;
        }
    }
}
