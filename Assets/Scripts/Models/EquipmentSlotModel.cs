using System.Xml.Serialization;

namespace Scripts.Models
{
    public class EquipmentSlotModel : ElementModel
    {
        [XmlElement]
        public ItemModel Item { get; set; }
    }
}
