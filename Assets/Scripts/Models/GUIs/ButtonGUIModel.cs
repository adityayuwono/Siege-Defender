using System;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.GUIs
{
    [Serializable]
    public class ButtonGUIModel : BaseGUIModel
    {
        [XmlElement]
        public TriggeredModel Trigger { get; set; }
    }
}
