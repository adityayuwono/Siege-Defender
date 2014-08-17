using Assets.Scripts.Views;
using Scripts.Components;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class ProjectileView : ProjectileBaseView
    {
        private readonly ProjectileViewModel _viewModel;

        public ProjectileView(ProjectileViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnShow()
        {
            base.OnShow();

            _viewModel.DoShooting += ShootProjectile;
            _viewModel.IsKinematic.OnChange += IsKinematic_OnChange;
        }

        protected override void OnHide()
        {
            _viewModel.DoShooting -= ShootProjectile;
            _viewModel.IsKinematic.OnChange -= IsKinematic_OnChange;
            
            base.OnHide();
        }

        private void ShootProjectile(ObjectView source, ObjectView target)
        {
            var targetTransform = target.Transform;
            Transform.LookAt(targetTransform);

            // Randommize direction
            var direction = new Vector3(Random.Range(-0.04f, 0.04f), Random.Range(-0.02f, 0.02f), 1);
            Rigidbody.AddRelativeForce(direction*5000f, ForceMode.Acceleration);
        }

        private void IsKinematic_OnChange()
        {
            GameObject.GetComponent<Rigidbody>().isKinematic = _viewModel.IsKinematic.GetValue();
        }
    }
}
