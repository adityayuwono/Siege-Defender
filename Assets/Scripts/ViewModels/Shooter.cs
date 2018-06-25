using System.Collections;
using Scripts.Contexts;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Models.Weapons;
using Scripts.ViewModels.Items;
using Scripts.ViewModels.Weapons;
using UnityEngine;

namespace Scripts.ViewModels
{
	public class Shooter : Interval<ProjectileBase>
	{
		public readonly Property<bool> IsReloading = new Property<bool>();
		public readonly Property<bool> IsShooting = new Property<bool>();

		private readonly ShooterModel _model;

		private float _accuracy;
		private ProjectileModel _projectileModel;
		private AoEModel _aoeModel;

		public Shooter(ShooterModel model, Player parent) : base(model, parent)
		{
			_model = model;

			Ammunition = new AdjustableProperty<float>("Ammunition", this);
			MaxAmmunition = new AdjustableProperty<float>("MaxAmmunition", this);

			if (_model.ProjectileId == null)
			{
				throw new EngineException(this, "Failed to find ProjectileId");
			}

			if (_model.Target == null)
			{
				throw new EngineException(this, "Failed to find Target");
			}

			Target = new Target(_model.Target, this);
			Elements.Add(Target);

			GetParent<Player>().SpeedUpDuration.OnChange += UpdateShootingSpeed;
		}

		public float ReloadDuration
		{
			get { return _projectileModel.Stats.ReloadTime; }
		}

		public float Accuracy
		{
			get { return 1 - (_accuracy -= _projectileModel.Stats.Deviation); }
			private set { _accuracy = value; }
		}

		public int Scatters
		{
			get { return _projectileModel.Stats.Scatters; }
		}

		public Object Target { get; private set; }

		#region Ammunition
		public readonly AdjustableProperty<float> Ammunition;
		public readonly AdjustableProperty<float> MaxAmmunition;

		private float AmmunitionProperty
		{
			get { return Ammunition.GetValue(); }
			set { Ammunition.SetValue(value); }
		}
		#endregion

		public void StartShooting()
		{
			IsShooting.SetValue(true);
		}

		public void StopShooting()
		{
			IsShooting.SetValue(false);
			Accuracy = _projectileModel.Stats.Accuracy;
		}

		public Projectile SpawnProjectile()
		{
			if (AmmunitionProperty > 0)
			{
				GameEndStatsManager.AddOneProjectile();
				AmmunitionProperty--;
				return GetProjectile(_projectileModel, this);
			}

			if (!IsReloading.GetValue())
			{
				IsReloading.SetValue(true);
				Root.StartCoroutine(Reload());
			}

			return null;
		}

		public void SpawnAoE(string aoeModelId, Vector3 position)
		{
			var projectile = GetObject<AoE>(_aoeModel.Id, _aoeModel);
			projectile.Activate(this);
			projectile.Show(position);
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			var projectileItemBinding = GetParent<IContext>().PropertyLookup.GetProperty<ProjectileItem>(_model.ProjectileId);
			if (projectileItemBinding == null)
			{
				throw new EngineException(this, string.Format("Path: {0}, is not a valid Object", _model.ProjectileId));
			}

			var projectileItem = projectileItemBinding.GetValue();

			_projectileModel = projectileItem.UpdateModel();

			if (!string.IsNullOrEmpty(_projectileModel.Stats.AoEId))
			{
				var aoeModel = DataContext.Instance.GetObjectModel(this, _projectileModel.Stats.AoEId);
				_aoeModel = (AoEModel) CreateDuplicateModel(aoeModel.Id, aoeModel);

				for (var i = 0; i < 2; i++)
				{
					_aoeModel.Stats.Damage[i] = _projectileModel.Stats.Damage[i] * _aoeModel.DamageMultiplier;
				}
			}
			
			UpdateProjectile();

			IsShooting.SetValue(false);
		}

		private void UpdateProjectile()
		{
			Interval.SetValue(_projectileModel.Stats.RoF);
			OnReload();
		}

		private Projectile GetProjectile(ProjectileModel projectileModel, Shooter parent)
		{
			var projectile = GetObject<Projectile>(projectileModel.Id, projectileModel);
			projectile.Activate(parent);
			projectile.Show();
			return projectile;
		}

		private IEnumerator Reload()
		{
			yield return new WaitForSeconds(ReloadDuration);
			OnReload();
		}

		private void OnReload()
		{
			Accuracy = _projectileModel.Stats.Accuracy;
			AmmunitionProperty = _projectileModel.Stats.Ammunition;
			MaxAmmunition.SetValue(_projectileModel.Stats.Ammunition);
			IsReloading.SetValue(false);
		}

		private void UpdateShootingSpeed()
		{
			var isSpeedUpActive = GetParent<Player>().SpeedUpDuration.GetValue() > 0;
			if (isSpeedUpActive)
			{
				Interval.SetValue(_projectileModel.Stats.RoF / 1.25f);
			}
			else
			{
				Interval.SetValue(_projectileModel.Stats.RoF);
			}

			StopShooting();
			StartShooting();
		}
	}
}