using Scripts.Interfaces;
using Scripts.Models;

namespace Scripts.ViewModels.Items
{
    public class DropableSlots : Element
    {
        public DropableSlots(DropableSlotsModel model, IHaveRoot parent) : base(model, parent)
        {
            
        }

        public virtual void HandleObjectDropped(Object droppedObject)
        {
            UnityEngine.Debug.Log("Object was dropped");
        }
    }
}
