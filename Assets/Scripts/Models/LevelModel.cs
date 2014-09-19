using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class LevelModel : BaseModel
    {
        [XmlAttribute]
        public float Interval { get; set; }

        [XmlAttribute]
        [DefaultValue(0)]
        public int LoopCount { get; set; }



        [XmlElement(ElementName = "Spawn", Type = typeof(SpawnModel))]
        public List<SpawnModel> SpawnSequence { get; set; }

        public LevelModel()
        {
            LoopCount = 0;
        }
    }
}
