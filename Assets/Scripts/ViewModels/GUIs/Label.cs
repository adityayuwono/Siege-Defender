using Scripts.Core;
using Scripts.Models.GUIs;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
	public class Label : Static
	{
		public readonly Property<string> Text = new Property<string>();

		private readonly LabelModel _model;

		public Label(LabelModel model, ViewModels.Base parent) : base(model, parent)
		{
			_model = model;
			Color = Color.white;
		}

		public int Size { get; protected set; }
		public Color Color { get; protected set; }
	}
}