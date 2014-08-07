using System.Xml.Serialization;

namespace Scripts.Models
{
    public class SpawnModel : BaseModel
    {
        [XmlAttribute]
        public string EnemyId { get; set; }
    }
}
