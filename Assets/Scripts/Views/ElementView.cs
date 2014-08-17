using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class ElementView : RigidbodyView
    {
        private readonly ElementViewModel _viewModel;
        private readonly ObjectView _parent;

        public ElementView(ElementViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;
        }

        protected override GameObject GetGameObject()
        {
            return _parent.Transform.FindChild(_viewModel.AssetId).gameObject;
        }

        protected override void SetPosition()
        {
        }
    }
}
