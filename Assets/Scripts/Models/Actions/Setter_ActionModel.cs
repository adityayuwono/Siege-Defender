using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
    public class Setter_ActionModel : Base_ActionModel
    {
        [XmlAttribute]
        public string Property { get; set; }

        [XmlAttribute]
        public string Value { get; set; }
    }
}
