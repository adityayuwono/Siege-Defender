using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class ProjectileView : ObjectView
    {
        private readonly ProjectileViewModel _viewModel;

        public ProjectileView(ProjectileViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;

            _viewModel.OnShootAction += ShootProjectile;
        }

        private void ShootProjectile(ObjectView source, ObjectView target)
        {
            var projectile = AttachController<ProjectileController>();
            projectile.OnHit += _viewModel.DamageEnemy;
            projectile.OnDeath += _viewModel.Destroy;
            projectile.AoE = _viewModel.AoE;


            projectile.Shoot(target.GameObject.transform);
        }
    }
}
