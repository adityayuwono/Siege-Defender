using System;
using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
    [Serializable]
    public class LabelGUIModel : Object_Model
    {
        [XmlAttribute]
        public string Font { get; set; }
    }
}
