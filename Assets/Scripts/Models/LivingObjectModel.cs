using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class LivingObjectModel : ObjectModel
    {
        [XmlAttribute]
        public int Health { get; set; }
    }
}
