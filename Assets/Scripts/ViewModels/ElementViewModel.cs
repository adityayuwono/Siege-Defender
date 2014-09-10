using Scripts.Models;

namespace Scripts.ViewModels
{
    public class ElementViewModel : ObjectViewModel
    {
        private readonly Element_Model _model;

        public ElementViewModel(Element_Model model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
