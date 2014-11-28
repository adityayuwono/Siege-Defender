using System;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models
{
    [Serializable]
    public class LoadSceneActionModel : BaseActionModel
    {
        [XmlAttribute]
        public string LevelId { get; set; }

        public LoadSceneActionModel()
        {
            Target = "";
        }
    }
}
