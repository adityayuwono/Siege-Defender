using System;
using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
    [Serializable]
    public class ShooterGUIModel : BaseGUIModel
    {
        [XmlAttribute]
        public string ShooterTarget { get; set; }

        [XmlAttribute]
        public string AimingAssetId { get; set; }
    }
}
