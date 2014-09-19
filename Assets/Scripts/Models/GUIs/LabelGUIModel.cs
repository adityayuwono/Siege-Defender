using System;
using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
    [Serializable]
    public class LabelGUIModel : ObjectModel
    {
        [XmlAttribute]
        public string Font { get; set; }
    }
}
