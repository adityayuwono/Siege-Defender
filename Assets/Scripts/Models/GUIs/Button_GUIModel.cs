﻿using System;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.GUIs
{
    [Serializable]
    public class Button_GUIModel : Base_GUIModel
    {
        [XmlElement]
        public Triggered_Model Trigger { get; set; }
    }
}
