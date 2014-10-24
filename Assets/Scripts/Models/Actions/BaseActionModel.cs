using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
    [Serializable]
    public class BaseActionModel : TargetPropertyModel
    {
        [XmlAttribute]
        public float Wait { get; set; }

        [XmlAttribute]
        [DefaultValue(false)]
        public bool IsInterruptable { get; set; }

        public BaseActionModel()
        {
            Wait = 0;
        }
    }
}
