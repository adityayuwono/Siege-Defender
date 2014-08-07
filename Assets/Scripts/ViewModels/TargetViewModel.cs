using Scripts.Models;

namespace Scripts.ViewModels
{
    public class TargetViewModel : ObjectViewModel
    {
        private readonly TargetModel _model;

        public TargetViewModel(TargetModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public int Index
        {
            get { return _model.Index; }
        }
    }
}
