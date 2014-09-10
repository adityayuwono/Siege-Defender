using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class Level_Model : Base_Model
    {
        [XmlAttribute]
        public float Interval { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Spawn", Type = typeof(Spawn_Model))]
        public List<Spawn_Model> SpawnSequence { get; set; }
    }
}
