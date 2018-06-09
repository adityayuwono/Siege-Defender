using Scripts.Core;
using Scripts.Models.GUIs;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
	public class LabelGUI : Object
	{
		private readonly LabelGUIModel _model;
		public readonly Property<string> Text = new Property<string>();

		public LabelGUI(LabelGUIModel model, Base parent) : base(model, parent)
		{
			_model = model;
			Color = Color.white;
		}

		public int Size { get; protected set; }
		public Color Color { get; protected set; }
	}
}