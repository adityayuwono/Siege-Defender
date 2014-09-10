using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
    public class Base_ActionModel : Base_Model
    {
        [XmlAttribute]
        public string Target { get; set; }
    }
}
