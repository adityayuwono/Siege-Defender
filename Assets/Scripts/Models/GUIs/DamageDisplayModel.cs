using System.Xml.Serialization;

namespace Scripts.Models
{
    public class DamageDisplayModel : Interval_Model
    {
        [XmlAttribute]
        public string DamageGUI { get; set; }
    }
}
