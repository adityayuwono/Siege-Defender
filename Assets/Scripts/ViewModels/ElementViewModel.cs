using Scripts.Models;

namespace Scripts.ViewModels
{
    public class ElementViewModel : ObjectViewModel
    {
        private readonly ElementModel _model;

        public ElementViewModel(ElementModel model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
