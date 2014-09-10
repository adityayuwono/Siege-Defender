using Scripts.Models;

namespace Scripts.ViewModels
{
    public class TargetViewModel : ObjectViewModel
    {
        private readonly Target_Model _model;

        public TargetViewModel(Target_Model model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public int Index
        {
            get { return _model.Index; }
        }
    }
}
