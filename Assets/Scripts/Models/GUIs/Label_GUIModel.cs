using System;
using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
    [Serializable]
    public class Label_GUIModel : Object_Model
    {
        [XmlAttribute]
        public string Font { get; set; }
    }
}
