using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class Level_Model : Base_Model
    {
        [XmlAttribute]
        public float Interval { get; set; }

        [XmlAttribute]
        [DefaultValue(0)]
        public int LoopCount { get; set; }



        [XmlElement(ElementName = "Spawn", Type = typeof(Spawn_Model))]
        public List<Spawn_Model> SpawnSequence { get; set; }

        public Level_Model()
        {
            LoopCount = 0;
        }
    }
}
