using System;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
    [Serializable]
    public class TargetPropertyModel : BaseModel
    {
        [XmlAttribute]
        public string Target { get; set; }

        [XmlAttribute]
        public string Property { get; set; }

        [XmlAttribute]
        public string Value { get; set; }
    }
}
