using Scripts.Helpers;
using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class ShooterViewModel : IntervalViewModel<ProjectileBaseViewModel>
    {
        private readonly ShooterModel _model;

        private ProjectileModel _projectileModel;

        public ShooterViewModel(ShooterModel model, PlayerViewModel parent) : base(model, parent)
        {
            _model = model;

            if (_model.ProjectileId == null)
                throw new EngineException(this, "Projectile Model is null");
            if (_model.Source == null)
                throw new EngineException(this, "Source Model is null");
            if (_model.Target == null)
                throw new EngineException(this, "Target Model is null");


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


        protected override void OnLoad()
        {
            base.OnLoad();

            _projectileModel = Root.GetObjectModel(_model.ProjectileId) as ProjectileModel;
        }

        public ProjectileViewModel SpawnProjectile()
        {
            var projectile = GetObject<ProjectileViewModel>(_model.ProjectileId);
            projectile.Activate();
            projectile.Show();

            return projectile;
        }

        public void SpawnAoE(string aoeModelId, Vector3 position)
        {
            var projectile = GetObject<AoEViewModel>(aoeModelId);
            projectile.SetPosition(position);
            projectile.Activate();
            projectile.Show();
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
