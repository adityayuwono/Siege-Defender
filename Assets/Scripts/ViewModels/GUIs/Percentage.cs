using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
	public class Percentage : BaseGUI
	{
		private readonly PercentageModel _model;
		public Property<float> Value;
		public Property<float> MaxValue;

		public Percentage(PercentageModel model, Base parent) 
			: base(model, parent)
		{
			_model = model;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			Value = GetParent<IContext>().PropertyLookup.GetBinding(_model.Value).GetPropertyAs<float>();
			MaxValue = GetParent<IContext>().PropertyLookup.GetBinding(_model.MaxValue).GetPropertyAs<float>();
		}
	}
}