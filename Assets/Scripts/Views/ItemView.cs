using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Views
{
	public class ItemView : ObjectView
	{
		private readonly Item _viewModel;
		private ObjectView _parent;

		public ItemView(Item viewModel, ObjectView parent)
			: base(viewModel, parent)
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

			var parentTransform = _parent.Transform;
			var parentItemTable = _parent.Transform.Find("ItemSlot");
			if (parentItemTable != null)
				parentTransform = parentItemTable;

			return parentTransform;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			GameObject.GetComponent<UISprite>().spriteName = _viewModel.BaseItem;
			GameObject.GetComponent<UIButton>().normalSprite = _viewModel.BaseItem;
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