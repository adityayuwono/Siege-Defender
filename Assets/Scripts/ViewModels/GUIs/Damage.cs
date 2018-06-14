using System;
using System.Globalization;
using Scripts.Models.GUIs;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
	public class Damage : Label
	{
		public Damage(DamageModel model, ViewModels.Base parent) : base(model, parent)
		{
		}

		public float HideDelay
		{
			get { return 0.5f; }
		}

		public void ShowDamage(float damage, bool isCrit, Vector3 position)
		{
			Color = isCrit ? Color.yellow : Color.white;
			Size = isCrit ? 25 : 20;
			Position = position;
			Text.SetValue(Math.Round(damage).ToString(CultureInfo.InvariantCulture));

			Activate();
			Show();
			Hide("Only Display for a short time");
		}
	}
}