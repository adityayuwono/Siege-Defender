using System;
using System.Collections.Generic;
using Scripts.Contexts;
using Scripts.Helpers;
using Scripts.Models;
using Scripts.Models.Weapons;
using UnityEngine;

namespace Scripts.ViewModels
{
	public class ProjectileItem : Item
	{
		private readonly ProjectileItemModel _model;

		private ProjectileModel _projectileModel;

		public ProjectileItem(ProjectileItemModel model, Object parent)
			: base(model, parent)
		{
			_model = model;
		}

		public string Stats { get; private set; }

		// Return BaseProjectile, multiplied by level and improved with enchantment
		public ProjectileModel GetProjectileModel()
		{
			if (_projectileModel != null)
			{
				return _projectileModel;
			}

			var baseProjectileModel = DataContext.GetObjectModel(this, _model.BaseItem) as ProjectileModel;
			if (baseProjectileModel == null)
			{
				throw new EngineException(this, string.Format("Failed to Find a projectile model with id: {0}", _model.BaseItem));
			}

			var newProjectileModel = Copier.CopyAs<ProjectileModel>(baseProjectileModel);
			newProjectileModel.Type = newProjectileModel.Id; // Assign appropriate Id
			newProjectileModel.Id = baseProjectileModel.Id + "_" + Guid.NewGuid();

			var overriderModel = _model.Enchantments;
			if (overriderModel != null)
			{
				#region Calculate for damage increase
				var originalSplitDamages = baseProjectileModel.Damage;
				var overrideSplitDamage = overriderModel.Damage;
				var augmentedDamages = new List<float>();

				for (var i = 0; i < originalSplitDamages.Length; i++)
				{
					var originalSplitDamage = (originalSplitDamages[i] * _model.Level) + overrideSplitDamage[i];
					augmentedDamages.Add(originalSplitDamage);
				}
				#endregion

				newProjectileModel.Damage = augmentedDamages.ToArray();
				newProjectileModel.Accuracy = Mathf.Min(newProjectileModel.Accuracy + overriderModel.Accuracy, 1f);
				newProjectileModel.ReloadTime = Mathf.Max(newProjectileModel.ReloadTime - overriderModel.ReloadTime, 1f);
				newProjectileModel.CriticalChance = Mathf.Min(newProjectileModel.CriticalChance + overriderModel.CriticalChance, 1f);
				newProjectileModel.CriticalDamageMultiplier = newProjectileModel.CriticalDamageMultiplier + overriderModel.CriticalDamageMultiplier;
				newProjectileModel.Scatters += overriderModel.Scatters;
				newProjectileModel.Ammunition += overriderModel.Ammunition;
				newProjectileModel.AoEId = overriderModel.AoEId;
			}

			Stats = string.Format("Damage: {0}\n\nRate of Fire: {1}\nAmmunition: {2}",
				newProjectileModel.Damage,
				newProjectileModel.RoF,
				newProjectileModel.Ammunition);

			// Register the new Model, to make sure it's available for duplication later
			DataContext.AddNewObjectModel(newProjectileModel);
			_projectileModel = newProjectileModel;

			return newProjectileModel;
		}
	}
}