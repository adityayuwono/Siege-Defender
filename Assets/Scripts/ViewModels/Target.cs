using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class Target : Object
    {
        private readonly TargetModel _model;

        private readonly int _index;

        public Target(int index, TargetModel model, Object parent) : base(model, parent)
        {
            _index = index;

            _model = model;
        }

        private Property<string> _indexBinding;
        protected override void OnLoad()
        {
            base.OnLoad();

            _indexBinding = GetParent<IContext>().PropertyLookup.GetProperty<string>(_model.Index);
        }

        public int Index
        {
            get { return _indexBinding.GetValue() == "ControlStyle1" ? _index : 0; }
        }
    }
}
