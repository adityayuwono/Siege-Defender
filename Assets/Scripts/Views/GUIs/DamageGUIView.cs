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
            BalistaContext.Instance.IntervalRunner.SubscribeToInterval(Hide, _viewModel.HideDelay, false);
            
            iTween.MoveTo(GameObject, _viewModel.Position + (Vector3.up*2f), _viewModel.HideDelay);
        }

        private void Hide()
        {
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(Hide);
            base.OnHide("Hiding DamageGUI");
        }
    }
}
