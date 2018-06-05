using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
	public class ValueDisplayGUIView : BaseGUIView
	{
		private readonly ValueDisplayGUI _viewModel;

		public ValueDisplayGUIView(ValueDisplayGUI viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_viewModel.Value.OnChange += Value_OnChange;
		}

		private void Value_OnChange()
		{
			var value = _viewModel.Value.GetValue();
			var maxValue = _viewModel.MaxValue.GetValue();

			UpdateValueDisplay(value, maxValue);
		}

		protected virtual void UpdateValueDisplay(float value, float maxValue)
		{
		}

		protected override void OnDestroy()
		{
			_viewModel.Value.OnChange -= Value_OnChange;

			base.OnDestroy();
		}
	}
}