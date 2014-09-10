using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class EnemyManagerModel : Interval_Model
    {
        [XmlAttribute]
        public string LevelId { get; set; }
    }
}
