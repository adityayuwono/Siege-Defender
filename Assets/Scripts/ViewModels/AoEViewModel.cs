using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class AoEViewModel : ProjectileBaseViewModel
    {
        private readonly AoE_Model _model;
        
        public AoEViewModel(AoE_Model model, ShooterViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public float Radius
        {
            get { return _model.Radius; }
        }

        public override float DeathDelay
        {
            get { return 1f; }
        }
        public override float HideDelay
        {
            get { return 0.05f; }
        }

        public override void Show()
        {
            base.Show();
            Hide("AoEs are hidden immediately");
        }

        public override void CollideWithTarget(ObjectViewModel targetObject, Vector3 collisionPosition, Vector3 contactPoint)
        {
            DamageEnemy(targetObject, contactPoint);
        }

        private Vector3 _position;
        public override Vector3 Position
        {
            get { return _position; }
        }
        
        public void SetPosition(Vector3 position)
        {
            _position = position;
        }
    }
}
