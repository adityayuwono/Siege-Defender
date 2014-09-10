using System.Xml.Serialization;

namespace Scripts.Models
{
    public class EquipmentSlot_Model : ElementModel
    {
        [XmlElement]
        public Item_Model Item { get; set; }
    }
}
