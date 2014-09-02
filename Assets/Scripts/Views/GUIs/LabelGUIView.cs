using Scripts.ViewModels.GUIs;
using UnityEngine;

namespace Scripts.Views.GUIs
{
    public class LabelGUIView : ObjectView
    {
        private readonly LabelGUI _viewModel;
        public LabelGUIView(LabelGUI viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        private UILabel _uiLabel;
        protected override void OnLoad()
        {
            base.OnLoad();

            _uiLabel = GameObject.GetComponent<UILabel>();
            _uiLabel.trueTypeFont = Resources.Load<Font>("Fonts/" + _viewModel.Font);
            _viewModel.Text.OnChange += Text_OnChange;
            Text_OnChange();
        }

        private void Text_OnChange()
        {
            _uiLabel.text = _viewModel.Text.GetValue();
        }
    }
}
