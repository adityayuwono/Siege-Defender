using System.Collections;
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
        public float ReloadDuration
        {
            get { return _projectileModel.ReloadTime; }
        }



        private float _accuracy;
        public float Accuracy
        {
            get { return 1 - (_accuracy -= _projectileModel.Deviation); }
            private set { _accuracy = value; }
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
            Ammunition = _projectileModel.Ammunition;
            Accuracy = _projectileModel.Accuracy;
        }


        public readonly Property<bool> IsReloading = new Property<bool>();
        public ProjectileViewModel SpawnProjectile()
        {
            if (Ammunition > 0)
            {
                Ammunition--;
                var projectile = GetObject<ProjectileViewModel>(_model.ProjectileId);
                projectile.Activate();
                projectile.Show();

                return projectile;
            }

            if (!IsReloading.GetValue())
            {
                IsReloading.SetValue(true);
                Root.StartCoroutine(Reload());
            }

            return null;
        }

        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(ReloadDuration);
            OnReload();
        }

        private void OnReload()
        {
            Accuracy = _projectileModel.Accuracy;
            Ammunition = _projectileModel.Ammunition;
            IsReloading.SetValue(false);
        }

        public void SpawnAoE(string aoeModelId, Vector3 position)
        {
            var projectile = GetObject<AoEViewModel>(aoeModelId);
            projectile.SetPosition(position);
            projectile.Activate();
            projectile.Show();
        }



        public int Ammunition { get; private set; }
        
        public readonly Property<bool> IsShooting = new Property<bool>();
        public void StartShooting()
        {
            IsShooting.SetValue(true);
        }

        public void StopShooting()
        {
            IsShooting.SetValue(false);
            Accuracy = _projectileModel.Accuracy;
        }
    }
}
