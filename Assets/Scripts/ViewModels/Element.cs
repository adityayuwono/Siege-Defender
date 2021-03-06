using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models;

namespace Scripts.ViewModels
{
	public class Element : Object
	{
		private readonly ElementModel _model;
		public Property<bool> VisibilityBinding;

		public Element(ElementModel model, IHaveRoot parent)
			: base(model, parent)
		{
			_model = model;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			if (string.IsNullOrEmpty(_model.IsVisible))
			{
				VisibilityBinding = new Property<bool>("VisibilityBinding");
				VisibilityBinding.SetValue(true);
			}
			else
			{
				VisibilityBinding = GetParent<IContext>().PropertyLookup.GetBinding(_model.IsVisible).GetPropertyAs<bool>();
			}
		}
	}
}