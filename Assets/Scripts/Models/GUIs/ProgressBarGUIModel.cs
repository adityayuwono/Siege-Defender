using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
    public class ProgressBarGUIModel : BaseGUIModel
    {
        [XmlAttribute]
        public string Progress { get; set; }

        [XmlAttribute]
        public string MaxProgress { get; set; }
    }
}
