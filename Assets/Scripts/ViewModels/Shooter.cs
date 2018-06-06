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

		public readonly Property<bool> IsReloading = new Property<bool>();
		public readonly Property<bool> IsShooting = new Property<bool>();

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
		}

		public float ReloadDuration
		{
			get { return _projectileModel.Reload; }
		}

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
			Accuracy = _projectileModel.Accuracy;
		}

		public Projectile SpawnProjectile()
		{
			if (AmmunitionProperty > 0)
			{
				AmmunitionProperty--;
				var projectile = GetProjectile(_projectileModel);
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

		public void SpawnAoE(string aoeModelId, Vector3 position)
		{
			var projectile = GetAoE(_aoeModel);
			projectile.Activate(this);
			projectile.Show(position);
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			var projectileItemBinding = GetParent<IContext>().PropertyLookup.GetProperty<ItemModel>(_model.ProjectileId);
			if (projectileItemBinding == null)
			{
				throw new EngineException(this, string.Format("Path: {0}, is not a valid Object", _model.ProjectileId));
			}

			var projectileItemModel = (ProjectileItemModel)projectileItemBinding.GetValue();
			var projectileItem = new ProjectileItem(projectileItemModel, this);

			_projectileModel = projectileItem.GetProjectileModel();

			if (!string.IsNullOrEmpty(_projectileModel.AoEId))
			{
				var aoeModel = DataContext.GetObjectModel(this, _projectileModel.AoEId);
				_aoeModel = (AoEModel) CreateDuplicateModel(aoeModel.Id, aoeModel);

				for (var i = 0; i < 2; i++)
				{
					_aoeModel.Damage[i] = _projectileModel.Damage[i] * _aoeModel.DamageMultiplier;
				}
			}
			
			UpdateProjectile();

			IsShooting.SetValue(false);
		}

		private void UpdateProjectile()
		{
			Interval.SetValue(_projectileModel.RoF);
			OnReload();
		}

		private Projectile GetProjectile(ProjectileModel projectileModel)
		{
			var projectileId = projectileModel.Id;
			var projectile = CheckInactiveObjects(projectileId) as Projectile ?? SpawnNewProjectile(projectileModel);
			return projectile;
		}

		private Projectile SpawnNewProjectile(ProjectileModel projectileModel)
		{
			var newProjectileModel = SpawnNewObject(projectileModel.Id, projectileModel);
			return newProjectileModel as Projectile;
		}

		private AoE GetAoE(AoEModel aoeModel)
		{
			var aoeModelId = aoeModel.Id;
			var projectile = CheckInactiveObjects(aoeModelId) as AoE ?? SpawnNewAoE(aoeModel);
			return projectile;
		}

		private AoE SpawnNewAoE(AoEModel aoeModel)
		{
			var newProjectileModel = SpawnNewObject(aoeModel.Id, aoeModel);
			return newProjectileModel as AoE;
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
	}
}