using Scripts.Models;
using Scripts.Models.GUIs;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
	public class DamageDisplayManager : Interval<Object>
	{
		private readonly DamageDisplayModel _model;

		public DamageDisplayManager(DamageDisplayModel model, Object parent) : base(model, parent)
		{
			_model = model;
		}

		public void DisplayDamage(float damage, bool isCrit, Vector3 position)
		{
			var damageGUI = GetObject<Damage>(_model.DamageGUI);
			damageGUI.ShowDamage(damage, isCrit, position);
		}

		public HealthBar CreateHealthBar()
		{
			var healthBarGUI = GetObject<HealthBar>(_model.HealthBarGUI);
			return healthBarGUI;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			SDRoot.DamageDisplay = this;
		}
	}
}