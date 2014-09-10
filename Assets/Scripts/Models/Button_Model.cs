using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.Actions;
using Scripts.Models.GUIs;

namespace Scripts.Models
{
    [Serializable]
    public class Button_Model : BaseGUIModel
    {
        [XmlElement(ElementName = "LoadScene", Type = typeof(LoadScene_ActionModel))]
        [XmlElement(ElementName = "Setter", Type = typeof(Setter_ActionModel))]
        public List<Base_ActionModel> Actions { get; set; }
    }
}
