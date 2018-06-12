using Scripts.Core;
using Scripts.Helpers;
using Scripts.ViewModels.Items;
using UnityEngine;
using UnityEngine.UI;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Views.Items
{
	public class ItemView : ObjectView
	{
		private readonly Item _viewModel;
		private ObjectView _parent;

		public ItemView(Item viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			if (parent == null)
			{
				throw new EngineException(this, "Parent is null");
			}

			_viewModel = viewModel;
			_parent = parent;

			_viewModel.ParentChanged += Parent_OnChanged;
		}

		protected override Transform GetParent()
		{
			if (_parent == null)
			{
				throw new EngineException(this, "Parent is null");
			}

			var parentTransform = _parent.Transform;
			var parentItemSlot = _parent.Transform.Find(_viewModel.ItemSlotRoots);
			if (parentItemSlot == null)
			{
				parentItemSlot = _parent.Transform.Find(InventoryView.ItemSlotRoots);
			}

			var parentItemTable = parentItemSlot;
			if (parentItemTable != null)
			{
				parentTransform = parentItemTable;
			}

			return parentTransform;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			GameObject.GetComponent<Image>().enabled = false;
			Transform.GetChild(0).GetComponent<Image>().sprite =
				SpriteLookup.Instance.GetSpriteByName(_viewModel.BaseItem);
		}

		protected override void OnShow()
		{
			base.OnShow();

			Transform.localPosition = Vector3.zero;
			Transform.localScale = Vector3.one;
		}

		protected override void OnDestroy()
		{
			_viewModel.ParentChanged -= Parent_OnChanged;

			base.OnDestroy();
		}

		private void Parent_OnChanged(Object newParent)
		{
			_parent = newParent.Root.GetView<ObjectView>(newParent);

			Transform.SetParent(GetParent());

			Transform.localPosition = Vector3.zero;
		}
	}
}