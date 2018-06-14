using Scripts.ViewModels.GUIs;

namespace Scripts.Views.GUIs
{
	public class PercentageView : BaseGUIView
	{
		private readonly Percentage _viewModel;

		public PercentageView(Percentage viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_viewModel.Value.OnChange += Value_OnChange;
		}

		protected override void OnDestroy()
		{
			_viewModel.Value.OnChange -= Value_OnChange;

			base.OnDestroy();
		}
		
		protected virtual void UpdateValueDisplay(float value, float maxValue)
		{
		}

		private void Value_OnChange()
		{
			var value = _viewModel.Value.GetValue();
			var maxValue = _viewModel.MaxValue.GetValue();

			UpdateValueDisplay(value, maxValue);
		}
	}
}