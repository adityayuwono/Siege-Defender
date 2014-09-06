using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models
{
    public class LoadScene_ActionModel : Action_Model
    {
        [XmlAttribute]
        public string SceneId { get; set; }
    }
}
