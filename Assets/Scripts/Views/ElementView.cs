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
            // We try to find matching child, if there's none, we instantiate from prefabs
            var tryFindChild = _parent.Transform.FindChild(_viewModel.AssetId);
            return tryFindChild == null ? base.GetGameObject() : tryFindChild.gameObject;
        }

        protected override void SetPosition()
        {
        }
    }
}
