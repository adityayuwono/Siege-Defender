using Scripts.ViewModels.GUIs;
using UnityEngine;

namespace Scripts.Views.GUIs
{
    public class DamageGUIView : LabelGUIView
    {
        private readonly DamageGUI _viewModel;
        public DamageGUIView(DamageGUI viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
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
