using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class AoE : ProjectileBase
    {
        private readonly AoEModel _model;
        
        public AoE(AoEModel model, Shooter parent) : base(model, parent)
        {
            _model = model;
        }

        public float Radius
        {
            get { return _model.Radius; }
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

        public override void CollideWithTarget(Object targetObject, Vector3 collisionPosition, Vector3 contactPoint)
        {
            DamageEnemy(targetObject, contactPoint);
        }
        
        public void SetPosition(Vector3 position)
        {
            Position = position;
        }
    }
}
