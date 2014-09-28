using System;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
    [Serializable]
    public class BaseActionModel : TargetPropertyModel
    {
        [XmlAttribute]
        public float WaitDuration { get; set; }

        public BaseActionModel()
        {
            WaitDuration = 0;
        }
    }
}
