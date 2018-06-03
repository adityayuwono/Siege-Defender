using System.Collections;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Models.Weapons;
using Scripts.ViewModels.Weapons;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class Shooter : Interval<ProjectileBase>
    {
        private readonly ShooterModel _model;

        private ProjectileModel _projectileModel;

        public Shooter(ShooterModel model, Player parent) : base(model, parent)
        {
            _model = model;

            Ammunition = new AdjustableProperty<float>("Ammunition", this);
            MaxAmmunition = new AdjustableProperty<float>("MaxAmmunition", this);

            if (_model.ProjectileId == null)
                throw new EngineException(this, "Failed to find ProjectileId");
            if (_model.Target == null)
                throw new EngineException(this, "Failed to find Target");

            Target = new Target(_model.Target, this);
            Elements.Add(Target);
        }


        public float ReloadDuration
        {
            get { return _projectileModel.Reload; }
        }



        private float _accuracy;
        public float Accuracy
        {
            get { return 1 - (_accuracy -= _projectileModel.Deviation); }
            private set { _accuracy = value; }
        }

        public int Scatters
        {
            get { return _projectileModel.Scatters; }
        }

        public Object Target { get; private set; }

        private Property<ObjectModel> _projectileBinding;
        protected override void OnLoad()
        {
            base.OnLoad();

            _projectileBinding = GetParent<IContext>().PropertyLookup.GetProperty<ObjectModel>(_model.ProjectileId);
            if (_projectileBinding == null)
                throw new EngineException(this, string.Format("Path: {0}, is not a valid Object", _model.ProjectileId));

            _projectileBinding.OnChange += Projectile_OnChange;
            Projectile_OnChange();
            
            IsShooting.SetValue(false);
        }

        public override void Show()
        {
            base.Show();

            Root.LogEvent("Weapon Usage", _projectileModel.Type, _projectileModel.Damage, 1);
        }

        protected override void OnDestroyed()
        {
            _projectileBinding.OnChange -= Projectile_OnChange;

            base.OnDestroyed();
        }

        private void Projectile_OnChange()
        {
            _projectileModel = _projectileBinding.GetValue() as ProjectileModel;
            Interval.SetValue(_projectileModel.RoF);
            OnReload();
        }

        public readonly Property<bool> IsReloading = new Property<bool>();
        public Projectile SpawnProjectile()
        {
            if (AmmunitionProperty > 0)
            {
                AmmunitionProperty--;
                var projectile = GetObject<Projectile>(_projectileModel.Id);
                projectile.Activate(this);
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
            AmmunitionProperty = _projectileModel.Ammunition;
            MaxAmmunition.SetValue(_projectileModel.Ammunition);
            IsReloading.SetValue(false);
        }

        public void SpawnAoE(string aoeModelId, Vector3 position)
        {
            var projectile = GetObject<AoE>(aoeModelId);
            projectile.Activate(this);
            projectile.Show(position);
        }

        #region Ammunition
        public readonly AdjustableProperty<float> Ammunition;
        public readonly AdjustableProperty<float> MaxAmmunition;
        private float AmmunitionProperty
        {
            get { return Ammunition.GetValue(); }
            set { Ammunition.SetValue(value); }
        }
        #endregion

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
