using System.Diagnostics;
using Scripts.Interfaces;

namespace Scripts.Views
{
    public class BaseView : IBase
    {
        private readonly ViewModels.BaseViewModel _viewModel;
        private readonly BaseView _parent;

        protected BaseView(ViewModels.BaseViewModel viewModel, BaseView parent)
        {
            _viewModel = viewModel;
            _parent = parent;

            _viewModel.OnShow += OnShow;
            _viewModel.OnHide += OnHide;
        }

        public string Id
        {
            get { return _viewModel.Id; }
        }


        protected virtual void OnShow() { }

        protected virtual void OnHide(string reason)
        {
            UnityEngine.Debug.Log(Id+", Hiding "+reason);
        }
    }
}
