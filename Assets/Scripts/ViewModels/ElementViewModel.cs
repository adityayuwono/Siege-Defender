using Scripts.Models;

namespace Scripts.ViewModels
{
    public class ElementViewModel : Object
    {
        private readonly Element_Model _model;

        public ElementViewModel(Element_Model model, Object parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
