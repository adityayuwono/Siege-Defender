using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
    public class Base_ActionModel : BaseModel
    {
        [XmlAttribute]
        public string Target { get; set; }
    }
}
