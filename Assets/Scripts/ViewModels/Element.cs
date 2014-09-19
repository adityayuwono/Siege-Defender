using Scripts.Models;

namespace Scripts.ViewModels
{
    public class Element : Object
    {
        private readonly ElementModel _model;

        public Element(ElementModel model, Object parent) : base(model, parent)
        {
            _model = model;
        }
    }
}
