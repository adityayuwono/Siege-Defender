using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
    public class ProgressBarGUIView : BaseGUIView
    {
        private readonly ProgressBarGUI _viewModel;

        public ProgressBarGUIView(ProgressBarGUI viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            _viewModel.ProgressProperty.OnChange += ProgressProperty_OnChange;

            _uiProgressBar = GameObject.GetComponent<UIProgressBar>();
        }

        private UIProgressBar _uiProgressBar;

        private void ProgressProperty_OnChange()
        {
            var value = _viewModel.ProgressProperty.GetValue();
            var maxValue = _viewModel.MaxProgressProperty.GetValue();

            _uiProgressBar.value = value/maxValue;
        }

        protected override void OnDestroy()
        {
            _viewModel.ProgressProperty.OnChange -= ProgressProperty_OnChange;

            base.OnDestroy();
        }
    }
}
