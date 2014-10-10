using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scripts.Models.GUIs
{
    [Serializable]
    public class ShooterGUIsModel : BaseGUIModel
    {
        [XmlElement(ElementName = "ShooterGUI", Type = typeof(ShooterGUIModel))]
        public List<ShooterGUIModel> ShooterGUIs { get; set; }
    }

    [Serializable]
    public class ShooterGUIModel : BaseGUIModel
    {
        [XmlAttribute]
        public string ShooterTarget { get; set; }

        [XmlAttribute]
        public string AimingAssetId { get; set; }
    }
}
