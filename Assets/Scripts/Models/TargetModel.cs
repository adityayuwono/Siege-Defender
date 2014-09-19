using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class TargetModel : ObjectModel
    {
        [XmlAttribute]
        public int Index { get; set; }
    }
}
