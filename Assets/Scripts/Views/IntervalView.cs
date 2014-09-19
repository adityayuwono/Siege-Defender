using System;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class IntervalView : ElementView
    {
        private readonly IntervalBase _viewModel;

        public IntervalView(IntervalBase viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected virtual void IntervalInvoked()
        {
            throw new NotImplementedException();
        }

        public void StartInterval()
        {
            BalistaContext.Instance.IntervalRunner.SubscribeToInterval(IntervalInvoked, _viewModel.Interval.GetValue());
        }

        public void StopInterval()
        {
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(IntervalInvoked);
        }

        protected override void OnDestroy()
        {
            StopInterval();

            base.OnDestroy();
        }
    }
}
