using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models.GUIs;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
	public class Label : Static
	{
		public readonly Property<string> Text = new Property<string>("Text");

		private readonly LabelModel _model;
		private IBinding _propertyBinding;

		public Label(LabelModel model, Base parent) : base(model, parent)
		{
			_model = model;
			Color = Color.clear;
		}

		public int Size { get; protected set; }
		public Color Color { get; protected set; }

		protected override void OnLoad()
		{
			base.OnLoad();

			_propertyBinding = GetParent<IContext>().PropertyLookup.GetBinding(_model.Text);
			if (_propertyBinding != null)
			{
				_propertyBinding.Bind(UpdateText);
				UpdateText();
			}
		}

		protected override void OnDestroyed()
		{
			if (_propertyBinding != null)
			{
				_propertyBinding.Unbind();
			}

			base.OnDestroyed();
		}

		private void UpdateText()
		{
			var bindingValue = _propertyBinding.GetValue();
			if (bindingValue != null)
			{
				Text.SetValue(bindingValue.ToString());
			};
		}
	}
}