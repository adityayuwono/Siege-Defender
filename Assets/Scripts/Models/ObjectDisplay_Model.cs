using System.Xml.Serialization;

namespace Scripts.Models
{
    public class ObjectDisplay_Model : Interval_Model
    {
        [XmlAttribute]
        public string ObjectId { get; set; }
    }
}
