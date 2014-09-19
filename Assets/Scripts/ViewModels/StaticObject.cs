using Scripts.Models;

namespace Scripts.ViewModels
{
    public class StaticObject : Object
    {
        private readonly ObjectModel _model;
        public StaticObject(ObjectModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
