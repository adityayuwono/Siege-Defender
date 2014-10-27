using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class SpecialEffect : Object
    {
        private readonly SpecialEffectModel _model;

        public SpecialEffect(SpecialEffectModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }

        public void ShowSpecialEffect(Vector3 position)
        {
            Position = position;
            
            Activate();
            Show();
            Hide("Only Display for a short time");
        }
        
        public void SetDeathDelay(float delay)
        {
            _model.DeathDelay = delay;
        }
    }
}
