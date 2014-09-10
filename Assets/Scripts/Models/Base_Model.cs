using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class Base_Model
    {
        [XmlAttribute]
        public string Id { get; set; }
    }
}
