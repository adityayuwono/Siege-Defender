using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Views
{
    public class Inventory_View : ElementView
    {
        private readonly Inventory_ViewModel _viewModel;
        private UITable _uiTable;

        public Inventory_View(Inventory_ViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;

            _viewModel.OnChildrenChanged += Children_OnChanged;
        }

        private void Children_OnChanged()
        {
            if (_uiTable != null)
                _uiTable.Reposition();
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            _uiTable = Transform.FindChild("ItemSlot").GetComponent<UITable>();
        }

        protected override void OnDestroy()
        {
            _viewModel.OnChildrenChanged -= Children_OnChanged;

            base.OnDestroy();
        }
    }

    public class Item_View : ObjectView
    {
        private readonly Item_ViewModel _viewModel;
        private ObjectView _parent;

        public Item_View(Item_ViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            if (parent == null)
                throw new EngineException(this, "Parent is null");

            _viewModel = viewModel;
            _parent = parent;

            _viewModel.OnParentChanged += OnParentChanged;
        }

        private void OnParentChanged(Object newParent)
        {
            _parent = newParent.Root.GetView<ObjectView>(newParent);

            Transform.parent = GetParent();
        }

        protected override Transform GetParent()
        {
            if (_parent == null)
                throw new EngineException(this, "Parent is null");

            Transform parentTransform = _parent.Transform;
            var parentItemTable = _parent.Transform.FindChild("ItemSlot");
            if (parentItemTable != null)
                parentTransform = parentItemTable;

            return parentTransform;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GameObject.GetComponent<UISprite>().spriteName = _viewModel.Base;
            GameObject.GetComponent<UIButton>().normalSprite = _viewModel.Base;
        }

        protected override void OnShow()
        {
            base.OnShow();

            Transform.localPosition = Vector3.zero;
            Transform.localScale = Vector3.one;
        }

        protected override void OnDestroy()
        {
            _viewModel.OnParentChanged -= OnParentChanged;

            base.OnDestroy();
        }
    }
}
