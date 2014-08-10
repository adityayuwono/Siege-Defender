using System;
using Scripts.Helpers;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class ShooterViewModel : IntervalViewModel
    {
        private readonly ShooterModel _model;

        private readonly ProjectileModel _projectileModel;

        public ShooterViewModel(ShooterModel model, PlayerViewModel parent) : base(model, parent)
        {
            _model = model;

            if (_model.ProjectileId == null)
                throw new EngineException(this, "Projectile Model is null");
            if (_model.Source == null)
                throw new EngineException(this, "Source Model is null");
            if (_model.Target == null)
                throw new EngineException(this, "Target Model is null");



            _projectileModel = Root.GetProjectileModel(_model.ProjectileId);
            
            Source = new ObjectViewModel(_model.Source, this);
            Children.Add(Source);

            Target = new TargetViewModel(_model.Target, this);
            Children.Add(Target);
        }

        public override float Interval
        {
            get { return _projectileModel.RoF; }
        }

        public int Index
        {
            get { return _model.Index; }
        }

        public ObjectViewModel Source { get; private set; }
        public ObjectViewModel Target { get; private set; }


        public ProjectileViewModel SpawnProjectile()
        {
            var duplicateProjectile = Copier.CopyAs<ProjectileModel>(_projectileModel);
            duplicateProjectile.Id = Guid.NewGuid().ToString();

            var projectile = new ProjectileViewModel(duplicateProjectile, this);
            projectile.Activate();

            return projectile;
        }

        public readonly Property<bool> IsShooting = new Property<bool>();
        public void StartShooting()
        {
            IsShooting.SetValue(true);
        }

        public void StopShooting()
        {
            IsShooting.SetValue(false);
        }
    }
}
