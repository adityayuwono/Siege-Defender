using Scripts.Helpers;
using Scripts.ViewModels.Enemies;
using UnityEngine;

namespace Scripts.Views
{
    public class LimbView : LivingObjectView
    {
        private readonly Limb _viewModel;
        private readonly EnemyBaseView _parent;

        public LimbView(Limb viewModel, EnemyBaseView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;
        }

        private GameObject _breakable;

        protected override void OnLoad()
        {
            base.OnLoad();

            var tryFindBreakableParts = Transform.FindChild("Breakable");
            if (tryFindBreakableParts != null)
                _breakable = tryFindBreakableParts.gameObject;
        }

        protected override void OnShow()
        {
            base.OnShow();

            _viewModel.DoBreakParts += BreakBrekables;
        }

        private void BreakBrekables()
        {
            if (_breakable != null)
                _breakable.SetActive(false);
        }

        protected override void OnHide(string reason)
        {
            _viewModel.DoBreakParts -= BreakBrekables;

            base.OnHide(reason);
        }

        protected override GameObject GetGameObject()
        {
            // We try to find matching child, if there's none, we instantiate from prefabs
            if (_parent == null)
                throw new EngineException(this, string.Format("Failed to find parent's Transform, parent is supposed to be: {0}", _viewModel.Parent.Id));

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
