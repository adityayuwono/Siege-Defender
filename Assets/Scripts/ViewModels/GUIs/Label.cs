﻿using Scripts.Core;
using Scripts.Interfaces;
using Scripts.Models.GUIs;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
	public class Label : Static
	{
		public readonly Property<string> Text = new Property<string>();

		private readonly LabelModel _model;
		private Binding _propertyBinding;

		public Label(LabelModel model, Base parent) : base(model, parent)
		{
			_model = model;
			Color = Color.white;
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
			Text.SetValue(_propertyBinding.GetValue().ToString());
		}
	}
}