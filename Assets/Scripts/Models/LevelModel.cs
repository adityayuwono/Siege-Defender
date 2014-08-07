using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models
{
    public class LevelModel : BaseModel
    {
        [XmlAttribute]
        public float Interval { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Spawn", Type = typeof(SpawnModel))]
        public List<SpawnModel> SpawnSequence { get; set; }
    }
}
