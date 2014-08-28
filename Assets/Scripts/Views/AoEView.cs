using System.Collections;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class AoEView : ProjectileBaseView
    {
        private readonly AoEViewModel _viewModel;
        public AoEView(AoEViewModel viewModel, ShooterView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            Freeze();
        }

        protected override void OnShow()
        {
            base.OnShow();

            Transform.localScale = Vector3.one*_viewModel.Radius/2f;
            Transform.parent = null;

            SetPosition();
        }

        protected override void OnHide(string reason)
        {
            _viewModel.Root.StartCoroutine(DelayedHiding(reason));
        }

        private IEnumerator DelayedHiding(string reason)
        {
            yield return new WaitForSeconds(_viewModel.HideDelay);
            HideAoE(reason);
        }

        protected virtual void HideAoE(string reason)
        {
            base.OnHide(reason);
        }
    }
}
