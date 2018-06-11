using Scripts.Core;
using Scripts.Models.GUIs;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
	public class LabelGUI : Object
	{
		public readonly Property<string> Text = new Property<string>();

		private readonly LabelGUIModel _model;
		private AdjustableProperty<string> _boundTextProperty;

		public LabelGUI(LabelGUIModel model, Base parent) : base(model, parent)
		{
			_model = model;
			Color = Color.white;
		}

		public int Size { get; protected set; }
		public Color Color { get; protected set; }
		public bool IsStatic
		{
			get { return _model.IsStatic; }
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			if (_model.Text.StartsWith("{"))
			{
				_boundTextProperty = Root.PropertyLookup.GetProperty(_model.Text) as AdjustableProperty<string>;
				_boundTextProperty.OnChange += HandlePropertyChanged;
			}
		}

		private void HandlePropertyChanged()
		{
			Text.SetValue(_boundTextProperty.GetValue());
		}
	}
}