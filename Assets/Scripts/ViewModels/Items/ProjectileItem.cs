using System;
using System.Collections.Generic;
using Scripts.Contexts;
using Scripts.Helpers;
using Scripts.Models.Items;
using Scripts.Models.Weapons;
using UnityEngine;

namespace Scripts.ViewModels.Items
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

		protected override void OnLoad()
		{
			base.OnLoad();

			GetProjectileModel();
		}

		public ProjectileModel GetProjectileModel(EnchantmentItemModel enchantmentModel)
		{
			_model.Enchantment = enchantmentModel;

			return GetProjectileModel();
		}

		public void DetachEnchantment()
		{
			_model.Enchantment = null;
		}

		// Return BaseProjectile, multiplied by level and improved with enchantment
		public ProjectileModel GetProjectileModel()
		{
			var baseProjectileModel = DataContext.GetObjectModel(this, _model.BaseItem) as ProjectileModel;
			if (baseProjectileModel == null)
			{
				throw new EngineException(this, string.Format("Failed to Find a projectile model with id: {0}", _model.BaseItem));
			}

			var newProjectileModel = Copier.CopyAs<ProjectileModel>(baseProjectileModel);
			newProjectileModel.Type = newProjectileModel.Id; // Assign appropriate Id
			newProjectileModel.Id = baseProjectileModel.Id + "_" + Guid.NewGuid();

			var overriderModel = _model.Enchantment;
			if (overriderModel != null)
			{
				#region Calculate for damage increase
				var originalSplitDamages = baseProjectileModel.Stats.Damage;
				var overrideSplitDamage = overriderModel.Stats.Damage;
				var augmentedDamages = new List<float>();

				for (var i = 0; i < originalSplitDamages.Length; i++)
				{
					var originalSplitDamage = (originalSplitDamages[i] * _model.Level) + overrideSplitDamage[i];
					augmentedDamages.Add(originalSplitDamage);
				}
				#endregion

				newProjectileModel.Stats.Damage = augmentedDamages.ToArray();
				newProjectileModel.Stats.Accuracy = Mathf.Min(newProjectileModel.Stats.Accuracy + overriderModel.Stats.Accuracy, 1f);
				newProjectileModel.Stats.ReloadTime = Mathf.Min(newProjectileModel.Stats.ReloadTime + overriderModel.Stats.ReloadTime, 1f);
				newProjectileModel.Stats.CriticalChance =
					Mathf.Min(
						newProjectileModel.Stats.CriticalChance + overriderModel.Stats.CriticalChance +
						(newProjectileModel.Stats.CriticalChance > 0 ? -1 : 0), 1f);
				newProjectileModel.Stats.CriticalDamageMultiplier = newProjectileModel.Stats.CriticalDamageMultiplier + overriderModel.Stats.CriticalDamageMultiplier;
				newProjectileModel.Stats.Scatters += overriderModel.Stats.Scatters;
				newProjectileModel.Stats.Ammunition += overriderModel.Stats.Ammunition;
				if (string.IsNullOrEmpty(newProjectileModel.Stats.AoEId))
				{
					newProjectileModel.Stats.AoEId = overriderModel.Stats.AoEId;
				}
			}

			var stats = "Damage\nSpeed\n\nRate of Fire\nAmmunition\nReload Time\n\nAccuracy\nRecoil";
			var numbers =
				string.Format(
					"{0}\n{6}\n\n{1}\n{2}\n{3}\n\n{4}\n{5}",
					string.Format("{0}-{1}", newProjectileModel.Stats.Damage[0], newProjectileModel.Stats.Damage[1]),
					newProjectileModel.Stats.RoF,
					newProjectileModel.Stats.Ammunition,
					newProjectileModel.Stats.ReloadTime,
					newProjectileModel.Stats.Accuracy,
					newProjectileModel.Stats.Deviation,
					string.Format("{0}-{1}", newProjectileModel.Stats.SpeedDeviation[0], newProjectileModel.Stats.SpeedDeviation[1])
				);

			if (newProjectileModel.Stats.CriticalChance > 0)
			{
				stats += "\n\nCritical Chance\nCritical Damage";
				numbers +=
					string.Format("\n\n{0}%\n{1}%",
						newProjectileModel.Stats.CriticalChance * 100,
						newProjectileModel.Stats.CriticalDamageMultiplier * 100
					);
			}

			if (newProjectileModel.Stats.Scatters > 1)
			{
				stats += "\n\nProjectiles Shot";
				numbers +=
					string.Format("\n\n{0}",
						newProjectileModel.Stats.Scatters
					);
			}

			Stats = stats;
			Numbers = numbers;

			// Register the new Model, to make sure it's available for duplication later
			DataContext.AddNewObjectModel(newProjectileModel);
			_projectileModel = newProjectileModel;

			return newProjectileModel;
		}
	}
}