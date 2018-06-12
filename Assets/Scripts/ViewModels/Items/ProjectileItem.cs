using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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

			UpdateModel();
		}

		public ProjectileModel UpdateModel(EnchantmentItemModel enchantmentModel)
		{
			_model.Enchantment = enchantmentModel;

			return UpdateModel();
		}

		public void DetachEnchantment()
		{
			_model.Enchantment = null;
		}

		// Return BaseProjectile, multiplied by level and improved with enchantment
		public ProjectileModel UpdateModel()
		{
			var baseProjectileModel = DataContext.GetObjectModel(this, _model.BaseItem) as ProjectileModel;
			if (baseProjectileModel == null)
			{
				throw new EngineException(this, string.Format("Failed to Find a projectile model with id: {0}", _model.BaseItem));
			}

			var newProjectileModel = Copier.CopyAs<ProjectileModel>(baseProjectileModel);
			newProjectileModel.Type = newProjectileModel.Id; // Assign appropriate Id
			newProjectileModel.Id = baseProjectileModel.Id + "_" + Guid.NewGuid();

			var enchantmentModel = _model.Enchantment;
			UpdateStats(newProjectileModel, enchantmentModel);

			if (enchantmentModel != null)
			{
				#region Calculate for damage increase
				var originalSplitDamages = baseProjectileModel.Stats.Damage;
				var overrideSplitDamage = enchantmentModel.Stats.Damage;
				var augmentedDamages = new List<float>();

				for (var i = 0; i < originalSplitDamages.Length; i++)
				{
					var originalSplitDamage = (originalSplitDamages[i] * _model.Level) + overrideSplitDamage[i];
					augmentedDamages.Add(originalSplitDamage);
				}
				#endregion

				newProjectileModel.Stats.Damage = augmentedDamages.ToArray();
				newProjectileModel.Stats.Accuracy = Mathf.Min(newProjectileModel.Stats.Accuracy + enchantmentModel.Stats.Accuracy, 1f);
				newProjectileModel.Stats.ReloadTime = Mathf.Min(newProjectileModel.Stats.ReloadTime + enchantmentModel.Stats.ReloadTime, 1f);
				newProjectileModel.Stats.CriticalChance =
					Mathf.Min(
						newProjectileModel.Stats.CriticalChance + enchantmentModel.Stats.CriticalChance +
						(newProjectileModel.Stats.CriticalChance > 0 && enchantmentModel.Stats.CriticalChance > 0 ? -1 : 0), 1f);
				newProjectileModel.Stats.CriticalDamageMultiplier = newProjectileModel.Stats.CriticalDamageMultiplier + enchantmentModel.Stats.CriticalDamageMultiplier;
				newProjectileModel.Stats.Scatters += enchantmentModel.Stats.Scatters;
				newProjectileModel.Stats.Ammunition += enchantmentModel.Stats.Ammunition;
				if (string.IsNullOrEmpty(newProjectileModel.Stats.AoEId))
				{
					newProjectileModel.Stats.AoEId = enchantmentModel.Stats.AoEId;
				}
			}

			// Register the new Model, to make sure it's available for duplication later
			DataContext.AddNewObjectModel(newProjectileModel);
			_projectileModel = newProjectileModel;

			return newProjectileModel;
		}

		private void UpdateStats(ProjectileModel projectileModel, EnchantmentItemModel enchantment)
		{
			var stats = "Damage\nSpeed\n\nRate of Fire\nAmmunition\nReload Time\n\nAccuracy\nRecoil";
			var numbers =
				string.Format(
					"{0}\n{6}\n\n{1}\n{2}\n{3}\n\n{4}\n{5}",
					string.Format("{0}-{1}", projectileModel.Stats.Damage[0], projectileModel.Stats.Damage[1]),
					projectileModel.Stats.RoF,
					projectileModel.Stats.Ammunition,
					projectileModel.Stats.ReloadTime,
					projectileModel.Stats.Accuracy,
					projectileModel.Stats.Deviation,
					string.Format("{0}-{1}", projectileModel.Stats.SpeedDeviation[0], projectileModel.Stats.SpeedDeviation[1])
				);
			var augmentation = "";
			if (enchantment != null)
			{
				augmentation =
					string.Format("{0}\n{6}\n\n{1}\n{2}\n{3}\n\n{4}\n{5}",
						string.Format("{0}-{1}", enchantment.Stats.Damage[0], enchantment.Stats.Damage[1]),
						enchantment.Stats.RoF,
						enchantment.Stats.Ammunition,
						enchantment.Stats.ReloadTime,
						enchantment.Stats.Accuracy,
						enchantment.Stats.Deviation,
						string.Format("{0}-{1}", enchantment.Stats.SpeedDeviation[0], enchantment.Stats.SpeedDeviation[1])
					);
			} 

			stats += "\n\nCrit Chance\nCrit Damage";
			numbers +=
				string.Format("\n\n{0}%\n{1}%",
					projectileModel.Stats.CriticalChance * 100,
					projectileModel.Stats.CriticalDamageMultiplier * 100
				);
			if (enchantment != null)
			{
				augmentation +=
					string.Format("\n\n{0}%\n{1}%",
						enchantment.Stats.CriticalChance * 100,
						enchantment.Stats.CriticalDamageMultiplier * 100
					);
			} 

			stats += "\n\nProjectiles";
			numbers +=
				string.Format("\n\n{0}",
					projectileModel.Stats.Scatters
				);
			if (enchantment != null)
			{
				augmentation +=
					string.Format("\n\n{0}",
						enchantment.Stats.Scatters
					);
			}

			Stats = stats;
			Numbers = numbers;
			Augmentation = augmentation;
		}
	}
}