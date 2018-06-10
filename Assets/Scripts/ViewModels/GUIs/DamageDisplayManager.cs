using Scripts.Models;
using Scripts.ViewModels.Enemies;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
	public class DamageDisplayManager : Interval<Object>
	{
		private readonly DamageDisplayGUIModel _model;

		public DamageDisplayManager(DamageDisplayGUIModel model, Object parent) : base(model, parent)
		{
			_model = model;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			SDRoot.DamageDisplay = this;
		}

		public void DisplayDamage(float damage, bool isCrit, Vector3 position)
		{
			var damageGUI = GetObject<DamageGUI>(_model.DamageGUI);
			damageGUI.ShowDamage(damage, isCrit, position);
		}

		public HealthBar CreateHealthBar()
		{
			var healthBarGUI = GetObject<HealthBar>(_model.HealthBarGUI);
			return healthBarGUI;
		}
	}
}