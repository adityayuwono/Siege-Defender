using Scripts.ViewModels;
using UnityEngine;

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

            _uiTable = Transform.FindChild("ItemTable").GetComponent<UITable>();
        }
    }

    public class Item_View : ObjectView
    {
        private readonly Item_ViewModel _viewModel;
        private ObjectView _parent;
        public Item_View(Item_ViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;

            _viewModel.OnParentChanged += OnParentChanged;
        }

        private void OnParentChanged(ObjectViewModel objectViewModel)
        {
            _parent = objectViewModel.Root.GetView<ObjectView>(objectViewModel.Id);
            _parent = newParent.Root.GetView<ObjectView>(newParent);

            Transform.parent = GetParent();

            Transform.localPosition = Vector3.zero;
            Transform.localScale = Vector3.one;
        }

        protected override Transform GetParent()
        {
            Transform parentTransform;
            if (_parent == null || _parent.GameObject == null)
                parentTransform = GameObject.Find("Context").transform;
            else
            {
                parentTransform = _parent.Transform;
                var parentItemTable = _parent.Transform.FindChild("ItemTable");
                if (parentItemTable != null)
                    parentTransform = parentItemTable;
            }

            return parentTransform;
        }

        protected override void OnShow()
        {
            base.OnShow();

            Transform.localPosition = Vector3.zero;
            Transform.localScale = Vector3.one;
        }
    }
}
