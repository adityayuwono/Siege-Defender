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

        private void ShootProjectile(ObjectView target, float accuracy)
        {
            SetRandomPosition();

            #region Randomize direction
            var direction = new Vector3(
                Random.Range(-5f, 5f) * accuracy,
                (Random.Range(-5f, 5f) * accuracy), 
                0f);// Z is the distance to the target, we don't randomize this, we randomize speed below instead
	        var targetPosition = target.Transform.position + direction;
            Transform.rotation = Quaternion.LookRotation(targetPosition - Transform.position);
            #endregion

            var randomForce = Random.Range(_viewModel.SpeedDeviations[0], _viewModel.SpeedDeviations[1]);

			if (_viewModel.IsRotationRandomized)
			{
				var randomRotation = new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
				Transform.localEulerAngles = (randomRotation);
			}

			AddRelativeForce(randomForce);
        }

        private void IsKinematic_OnChange()
        {
            GameObject.GetComponent<Rigidbody>().isKinematic = _viewModel.IsKinematic.GetValue();
        }
    }
}
