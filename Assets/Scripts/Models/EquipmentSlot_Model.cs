using System.Xml.Serialization;

namespace Scripts.Models
{
    public class EquipmentSlot_Model : Element_Model
    {
        [XmlElement]
        public Item_Model Item { get; set; }
    }
}
