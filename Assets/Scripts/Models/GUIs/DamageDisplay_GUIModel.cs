using System.Xml.Serialization;

namespace Scripts.Models
{
    public class DamageDisplay_GUIModel : Interval_Model
    {
        [XmlAttribute]
        public string DamageGUI { get; set; }
    }
}
