using Scripts.Interfaces;

namespace Scripts.Views
{
    public class BaseView : IBase
    {
        private readonly ViewModels.BaseViewModel _viewModel;
        protected readonly BaseView Parent;

        protected BaseView(ViewModels.BaseViewModel viewModel, BaseView parent)
        {
            _viewModel = viewModel;
            Parent = parent;

            _viewModel.OnShow += OnShow;
            _viewModel.OnHide += OnHide;
        }

        public string Id
        {
            get { return _viewModel.Id; }
        }


        protected virtual void OnShow() { }
        protected virtual void OnHide() { }
    }
}
