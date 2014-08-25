using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models
{
    [Serializable]
    public class ObjectModel : BaseModel
    {
        // Not XML Property, this is set when we spawn it
        public string Type;

        [XmlAttribute]
        public string AssetId { get; set; }

        [XmlAttribute]
        [DefaultValue("0,0,0")]
        public string Position { get; set; }

        public ObjectModel()
        {
            Position = "0,0,0";
        }
    }
}
