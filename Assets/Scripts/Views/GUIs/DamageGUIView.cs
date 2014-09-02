using System.Collections;
using Scripts.ViewModels.GUIs;
using UnityEngine;

namespace Scripts.Views.GUIs
{
    public class DamageGUIView : LabelGUIView
    {
        private DamageGUI _viewModel;
        public DamageGUIView(DamageGUI viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        private Vector3 _localScale;
        protected override void OnLoad()
        {
            base.OnLoad();

            // Need to cache because we are going to animate this
            _localScale = Transform.localScale;
        }

        protected override void OnShow()
        {
            base.OnShow();

            Transform.localScale = _localScale;
        }

        protected override void OnHide(string reason)
        {
            _viewModel.Root.StartCoroutine(DelayedHiding(reason));

            iTween.ScaleTo(GameObject, Vector3.zero, _viewModel.HideDelay);
            iTween.MoveTo(GameObject, _viewModel.Position + (Vector3.up*2f), _viewModel.HideDelay);
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
