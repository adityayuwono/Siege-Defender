using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class SpecialEffect : Object
    {
        private SpecialEffectModel _model;
        public SpecialEffect(SpecialEffectModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }

        public void ShowSpecialEffect(Vector3 position)
        {
            _position = position;
            
            Activate();
            Show();
            Hide("Only Display for a short time");
        }

        private Vector3 _position;
        public override Vector3 Position
        {
            get { return _position; }
        }

        public void SetDeathDelay(float delay)
        {
            _model.DeathDelay = delay;
        }
    }
}
