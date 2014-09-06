using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models
{
    [Serializable]
    public class Button_Model : ElementModel
    {
        [XmlElement(ElementName = "LoadScene", Type = typeof(LoadScene_ActionModel))]
        public List<Action_Model> Actions { get; set; }
    }
}
