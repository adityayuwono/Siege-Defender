using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
	public class ElementView : RigidbodyView
	{
		private readonly ObjectView _parent;
		private readonly Element _viewModel;

		public ElementView(Element viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
			_parent = parent;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_viewModel.VisibilityBinding.OnChange += UpdateVisibility;
			UpdateVisibility();
		}

		protected override void OnDestroy()
		{
			_viewModel.VisibilityBinding.OnChange -= UpdateVisibility;

			base.OnDestroy();
		}

		private void UpdateVisibility()
		{
			GameObject.SetActive(_viewModel.VisibilityBinding.GetValue());
		}

		protected override GameObject GetGameObject()
		{
			// We try to find matching child, if there's none, we instantiate from prefabs
			if (_parent == null)
			{
				throw new EngineException(this,
					string.Format("Failed to find parent's Transform, parent is supposed to be: {0}", _viewModel.Parent.Id));
			}

			var tryFindChild = _parent.Transform.FindChildRecursivelyBreadthFirst(_viewModel.AssetId);
			return tryFindChild == null ? base.GetGameObject() : tryFindChild.gameObject;
		}

		protected override Transform GetParent()
		{
			return GameObject.transform.parent != null ? GameObject.transform.parent : base.GetParent();
		}

		protected override void SetPosition()
		{
			// Assume asset's position
		}
	}
}