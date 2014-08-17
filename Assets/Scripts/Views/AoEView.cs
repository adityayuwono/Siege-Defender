using Assets.Scripts.Views;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class AoEView : ProjectileBaseView
    {
        private readonly AoEViewModel _viewModel;
        private readonly ProjectileView _parent;
        public AoEView(AoEViewModel viewModel, ProjectileView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;
        }

        protected override void OnShow()
        {
            base.OnShow();

            Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            Rigidbody.isKinematic = false;

            GameObject.renderer.enabled = true;
            
            Transform.localScale = Vector3.one*_viewModel.Radius/2f;
            Transform.parent = null;
        }

        protected override void OnHide()
        {
            base.OnHide();
        }

        protected override GameObject GetGameObject()
        {
            return _parent.Transform.FindChild("AoE").gameObject;
        }

        protected override void SetPosition()
        {
            GameObject.transform.localPosition = Vector3.zero;
        }
    }
}
