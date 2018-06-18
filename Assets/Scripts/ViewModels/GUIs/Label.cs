using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models.GUIs;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
	public class Label : Static
	{
		public readonly Property<string> Text = new Property<string>();

		private readonly LabelModel _model;
		private Property<int> _propertyBinding;

		public Label(LabelModel model, Base parent) : base(model, parent)
		{
			_model = model;
			Color = Color.white;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_propertyBinding = GetParent<IContext>().PropertyLookup.GetProperty(_model.Text) as Property<int>;
			if (_propertyBinding != null)
			{
				_propertyBinding.OnChange += UpdateText;
				UpdateText();
			}
		}

		protected override void OnDestroyed()
		{
			if (_propertyBinding != null)
			{
				_propertyBinding.OnChange -= UpdateText;
			}

			base.OnDestroyed();
		}

		private void UpdateText()
		{
			Text.SetValue(_propertyBinding.GetValue().ToString());
		}

		public int Size { get; protected set; }
		public Color Color { get; protected set; }
	}
}