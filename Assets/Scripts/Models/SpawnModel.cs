using System.Xml.Serialization;

namespace Scripts.Models
{
    public class SpawnModel : Base_Model
    {
        [XmlAttribute]
        public string EnemyId { get; set; }
    }
}
