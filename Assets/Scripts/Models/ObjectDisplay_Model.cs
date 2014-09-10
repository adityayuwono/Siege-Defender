using System.Xml.Serialization;

namespace Scripts.Models
{
    public class ObjectDisplay_Model : IntervalModel
    {
        [XmlAttribute]
        public string ObjectId { get; set; }
    }
}
