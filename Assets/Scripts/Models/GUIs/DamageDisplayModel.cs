using System.Xml.Serialization;

namespace Scripts.Models
{
    public class DamageDisplayModel : IntervalModel
    {
        [XmlAttribute]
        public string DamageGUI { get; set; }
    }
}
