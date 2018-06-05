using System;
using System.Collections.Generic;
using System.Globalization;
using Scripts.Helpers;
using Scripts.Models;
using Scripts.Models.Weapons;

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

		public ProjectileModel GetProjectileModel()
		{
			if (_projectileModel != null) return _projectileModel;

			var baseProjectileModel = DataContext.GetObjectModel(this, _model.Base) as ProjectileModel;
			if (baseProjectileModel == null)
				throw new EngineException(this, string.Format("Failed to Find a projectile model with id: {0}", _model.Base));

			var newProjectileModel = Copier.CopyAs<ProjectileModel>(baseProjectileModel);
			newProjectileModel.Type = newProjectileModel.Id; // Assign appropriate Id
			newProjectileModel.Id = baseProjectileModel.Id + "_" + Guid.NewGuid();

			var overriderModel = _model.Overrides;
			if (overriderModel != null)
			{
				#region Calculate for damage increase

				var originalSplitDamages = baseProjectileModel.Damage.Split('-');
				var overrideSplitDamage = overriderModel.Damage.Split('-');
				var augmentedDamages = new List<string>();

				for (var i = 0; i < originalSplitDamages.Length; i++)
				{
					var originalSplitDamage = float.Parse(originalSplitDamages[i]) + float.Parse(overrideSplitDamage[i]);
					augmentedDamages.Add(originalSplitDamage.ToString(CultureInfo.InvariantCulture));
				}

				#endregion

				newProjectileModel.Damage = string.Join("-", augmentedDamages.ToArray());
				newProjectileModel.Scatters += overriderModel.Scatters;
				newProjectileModel.Ammunition += overriderModel.Ammunition;
			}

			Stats = string.Format("Damage: {0}\n\nRate of Fire: {1}\nAmmunition: {2}",
				newProjectileModel.Damage,
				newProjectileModel.RoF,
				newProjectileModel.Ammunition);

			// Register the new Model, to make sure it's available for duplication later
			DataContext.AddNewObjectModel(newProjectileModel);
			_projectileModel = new ProjectileModel();

			return newProjectileModel;
		}
	}
}