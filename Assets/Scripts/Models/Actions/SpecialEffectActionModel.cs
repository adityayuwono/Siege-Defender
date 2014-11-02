using System;
using System.Xml.Serialization;

namespace Scripts.Models.Actions
{
    [Serializable]
    public class SpecialEffectActionModel : SetterActionModel
    {
        [XmlAttribute]
        public string SpecialEffectId { get; set; }

        public SpecialEffectActionModel()
        {
            Target = "{This.SpecialEffect}";
        }
    }
}
