using Scripts.Core;
using Scripts.Models.GUIs;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
    public class LabelGUI : Object
    {
	    public readonly Property<string> Text = new Property<string>();
	    public Color Color;

        private readonly LabelGUIModel _model;
        
	    public LabelGUI(LabelGUIModel model, Base parent) : base(model, parent)
        {
            _model = model;
			Color = Color.white;
        }
    }
}
