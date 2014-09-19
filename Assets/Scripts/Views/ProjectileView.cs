using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class ProjectileView : ProjectileBaseView
    {
        private readonly Projectile _viewModel;

        public ProjectileView(Projectile viewModel, ShooterView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            _viewModel.DoShooting += ShootProjectile;
            _viewModel.IsKinematic.OnChange += IsKinematic_OnChange;
        }

        protected override void OnDestroy()
        {
            _viewModel.IsKinematic.OnChange -= IsKinematic_OnChange;
            _viewModel.DoShooting -= ShootProjectile;

            base.OnDestroy();
        }

        private void ShootProjectile(ObjectView source, ObjectView target, float accuracy)
        {
            Transform.position = source.Transform.position;

            var targetTransform = target.Transform;
            Transform.LookAt(targetTransform);

            // Randomize direction
            var direction = new Vector3(
                Random.Range(-0.2f, 0.2f) * accuracy,
                Random.Range(-0.1f, 0.1f) * accuracy, 
                1f);

            AddRelativeForce(direction*300f);
        }

        private void IsKinematic_OnChange()
        {
            GameObject.GetComponent<Rigidbody>().isKinematic = _viewModel.IsKinematic.GetValue();
        }
    }
}
