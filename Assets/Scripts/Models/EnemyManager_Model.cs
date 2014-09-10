using System;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class EnemyManager_Model : Interval_Model
    {
        [XmlAttribute]
        public string LevelId { get; set; }
    }
}
