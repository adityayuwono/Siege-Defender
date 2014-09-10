using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.GUIs
{
    [Serializable]
    public class Button_Model : Base_GUIModel
    {
        [XmlElement(ElementName = "LoadScene", Type = typeof(LoadScene_ActionModel))]
        [XmlElement(ElementName = "Setter", Type = typeof(Setter_ActionModel))]
        public List<Base_ActionModel> Actions { get; set; }
    }
}
