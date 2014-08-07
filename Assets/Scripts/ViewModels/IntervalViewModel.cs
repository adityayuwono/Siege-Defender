using Scripts.Models;

namespace Scripts.ViewModels
{
    public class IntervalViewModel : ElementViewModel
    {
        private readonly IntervalModel _model;

        public IntervalViewModel(IntervalModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public virtual float Interval
        {
            get { return _model.Interval; }
        }
    }
}
