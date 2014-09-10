using System.Xml.Serialization;

namespace Scripts.Models
{
    public class Spawn_Model : Base_Model
    {
        [XmlAttribute]
        public string EnemyId { get; set; }
    }
}
