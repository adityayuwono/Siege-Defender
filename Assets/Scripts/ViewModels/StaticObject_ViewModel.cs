using Scripts.Models;

namespace Scripts.ViewModels
{
    public class StaticObject_ViewModel : ObjectViewModel
    {
        private readonly ObjectModel _model;
        public StaticObject_ViewModel(ObjectModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
