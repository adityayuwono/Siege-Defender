﻿using Scripts.Core;
using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class LabelGUI : ObjectViewModel
    {
        private LabelGUIModel _model;
        public LabelGUI(LabelGUIModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public readonly Property<string> Text = new Property<string>();

        public string Font
        {
            get { return _model.Font; }
        }
    }
}