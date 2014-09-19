using Scripts.Models;

namespace Scripts.ViewModels
{
    public class StaticObject_ViewModel : Object
    {
        private readonly Object_Model _model;
        public StaticObject_ViewModel(Object_Model model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
