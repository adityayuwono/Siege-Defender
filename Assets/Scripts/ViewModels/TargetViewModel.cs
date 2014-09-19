using Scripts.Models;

namespace Scripts.ViewModels
{
    public class TargetViewModel : Object
    {
        private readonly Target_Model _model;

        public TargetViewModel(Target_Model model, Object parent) : base(model, parent)
        {
            _model = model;
        }

        public int Index
        {
            get { return _model.Index; }
        }
    }
}
