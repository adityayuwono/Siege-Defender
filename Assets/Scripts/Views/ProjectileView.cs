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

            // Randomize direction
            var direction = new Vector3(
                Random.Range(-5f, 5f) * accuracy,
                (Random.Range(-5f, 5f) * accuracy), 
                1f);
            targetTransform.position = targetTransform.position + direction;

            Transform.LookAt(targetTransform);

            var randomForce = Random.Range(_viewModel.SpeedDeviations[0], _viewModel.SpeedDeviations[1]);
            AddRelativeForce(new Vector3(0,0,1) * randomForce);

            if (_viewModel.IsRotationRandomized)
            {
                var randomRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
                Transform.localEulerAngles = (randomRotation);
            }
        }

        private void IsKinematic_OnChange()
        {
            GameObject.GetComponent<Rigidbody>().isKinematic = _viewModel.IsKinematic.GetValue();
        }
    }
}
