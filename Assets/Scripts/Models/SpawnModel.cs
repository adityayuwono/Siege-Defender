using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models
{
    public class SpawnModel : BaseModel
    {
        [XmlAttribute]
        [DefaultValue(1)]
        public int Count { get; set; }

        [XmlAttribute]
        public string EnemyId { get; set; }

        public SpawnModel()
        {
            Count = 1;
        }
    }
}
