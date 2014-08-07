using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class IntervalView : ElementView
    {
        private readonly IntervalViewModel _viewModel;

        public IntervalView(IntervalViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected T AttachIntervalController<T>() where T : IntervalController
        {
            var intervalController = base.AttachController<T>();
            intervalController.OnInterval += IntervalInvoked;

            return intervalController;
        }

        protected virtual void IntervalInvoked()
        {
            throw new System.NotImplementedException();
        }
    }
}
