using System;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
    [Serializable]
    public class BaseActionModel : TargetPropertyModel
    {
        [XmlAttribute]
        public float Wait { get; set; }

        public BaseActionModel()
        {
            Wait = 0;
        }
    }
}
