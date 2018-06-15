using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
	public class Percentage : BaseGUI
	{
		private readonly PercentageModel _model;
		public Property<float> MaxValue;

		public Property<float> Value;

		public Percentage(PercentageModel model, Base parent) 
			: base(model, parent)
		{
			_model = model;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			Value = GetParent<IContext>().PropertyLookup.GetProperty<float>(_model.Value);
			MaxValue = GetParent<IContext>().PropertyLookup.GetProperty<float>(_model.MaxValue);
		}
	}
}