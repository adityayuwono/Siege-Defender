using System;
using Scripts.Core;
using Scripts.Models;

namespace Scripts.ViewModels
{
	public class Player : Element
	{
		public event Action OnGameOver;

		public readonly AdjustableProperty<float> Health;
		public readonly AdjustableProperty<float> MaxHealth;

		public Player(PlayerModel model, Scene parent) : base(model, parent)
		{
			Health = new AdjustableProperty<float>("Health", this);
			MaxHealth = new AdjustableProperty<float>("MaxHealth", this);
			MaxHealth.SetValue(model.Health);

			foreach (var shooterModel in model.Shooters)
			{
				Elements.Add(new Shooter(shooterModel, this));
			}
		}

		protected override void OnActivate()
		{
			Health.SetValue(MaxHealth.GetValue());

			Health.OnChange += OnDamaged;

			base.OnActivate();
		}

		protected override void OnDeactivate()
		{
			Health.OnChange -= OnDamaged;

			base.OnDeactivate();
		}

		private void OnDamaged()
		{
			var currentHealth = Health.GetValue();
			if (currentHealth <= 0)
			{
				if (OnGameOver != null)
				{
					// Player's health is 0, this means game over :( (
					OnGameOver();
				}
			}
		}
	}
}