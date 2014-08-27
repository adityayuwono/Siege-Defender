using System.Collections;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class AoEView : ProjectileBaseView
    {
        private readonly AoEViewModel _viewModel;
        private readonly ShooterView _parent;
        public AoEView(AoEViewModel viewModel, ShooterView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;
        }

        protected override void OnShow()
        {
            base.OnShow();

            GameObject.renderer.enabled = true;
            Transform.localScale = Vector3.one*_viewModel.Radius/2f;
            Transform.parent = null;

            SetPosition();
            Freeze();
        }

        protected override void OnHide(string reason)
        {
            _viewModel.Root.StartCoroutine(DelayedHiding(reason));
        }

        private IEnumerator DelayedHiding(string reason)
        {
            yield return new WaitForSeconds(_viewModel.DeathDelay);
            base.OnHide(reason);
        }
    }
}
