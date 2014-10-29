using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
    [Serializable]
    public class TargetPropertyModel : BaseModel
    {
        [XmlAttribute]
        [DefaultValue("{Monster}")]
        public string Target { get; set; }
        
        [XmlAttribute]
        public string Value { get; set; }

        public TargetPropertyModel()
        {
            Target = "{Monster}";
        }
    }
}
