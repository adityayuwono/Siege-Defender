using Scripts.Models;

namespace Scripts.ViewModels
{
    public class ElementViewModel : ObjectViewModel
    {
        private readonly ElementModel _model;

        public ElementViewModel(ElementModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
