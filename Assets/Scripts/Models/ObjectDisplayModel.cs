using System.Xml.Serialization;

namespace Scripts.Models
{
    public class ObjectDisplayModel : IntervalModel
    {
        [XmlAttribute]
        public string ObjectId { get; set; }
    }
}
