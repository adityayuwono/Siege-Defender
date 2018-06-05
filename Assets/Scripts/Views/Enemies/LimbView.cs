using Scripts.Helpers;
using Scripts.ViewModels.Enemies;
using UnityEngine;

namespace Scripts.Views.Enemies
{
	public class LimbView : LivingObjectView
	{
		private readonly EnemyView _parent;
		private readonly Limb _viewModel;

		private GameObject _breakable;

		public LimbView(Limb viewModel, EnemyView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
			_parent = parent;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			var tryFindBreakableParts = Transform.Find("Breakable");
			if (tryFindBreakableParts != null)
			{
				_breakable = tryFindBreakableParts.gameObject;
				if (_breakable != null) _breakable.SetActive(true);
			}
		}

		protected override void OnShow()
		{
			base.OnShow();

			_viewModel.OnBreak += BreakBrekables;
		}

		private void BreakBrekables()
		{
			if (_breakable != null) _breakable.SetActive(false);
		}

		protected override void OnHide(string reason)
		{
			_viewModel.OnBreak -= BreakBrekables; // :D

			base.OnHide(reason);
		}

		protected override void OnDestroy()
		{
			_breakable = null;

			base.OnDestroy();
		}

		protected override GameObject GetGameObject()
		{
			// We try to find matching child, if there's none, we instantiate from prefabs
			if (_parent == null)
				throw new EngineException(this,
					string.Format("Failed to find parent's Transform, parent is supposed to be: {0}", _viewModel.Parent.Id));

			// Go deeper to look
			var tryFindChild = _parent.Transform.FindChildRecursivelyBreadthFirst(_viewModel.AssetId);
			return tryFindChild.gameObject;
		}

		protected override Transform GetParent()
		{
			return GameObject.transform.parent;
		}

		protected override void SetPosition()
		{
			// Assume asset's position
		}
	}
}