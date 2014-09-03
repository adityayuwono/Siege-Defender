using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class PiercingProjectile_ViewModel : ProjectileViewModel
    {
        private readonly PiercingProjectile_Model _model;
        private readonly ShooterViewModel _parent;
        public PiercingProjectile_ViewModel(PiercingProjectile_Model model, ShooterViewModel parent) : base(model, parent)
        {
            _model = model;
            _parent = parent;
        }

        public override void Show()
        {
            base.Show();

            Hide("Hide from show :D");
        }

        public override void CollideWithTarget(ObjectViewModel targetObject, Vector3 collisionPosition, Vector3 contactPoint)
        {
            // Spawn AoE if there are any Id defined
            if (!string.IsNullOrEmpty(_model.AoEId))
                _parent.SpawnAoE(_model.AoEId, collisionPosition);

            DamageEnemy(targetObject, contactPoint, false);
        }

        public override float HideDelay
        {
            get { return 2f; }
        }
    }
}
