using System;
using System.Xml.Serialization;
using Scripts.Models.Actions;

namespace Scripts.Models.Enemies
{
    [Serializable]
    public class LimbModel : LivingObjectModel
    {
        [XmlElement]
        public TriggeredModel Trigger { get; set; }

        public LimbModel()
        {
            ProjectileLimit = 5;
        }
    }
}
