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
		public readonly AdjustableProperty<string> PowerUp;

		public readonly AdjustableProperty<float> DamageUpDuration;
		public readonly AdjustableProperty<float> SpeedUpDuration;
		public readonly AdjustableProperty<float> PowerUpMaxDuration;

		public Player(PlayerModel model, Scene parent) : base(model, parent)
		{
			Health = new AdjustableProperty<float>("Health", this);
			MaxHealth = new AdjustableProperty<float>("MaxHealth", this);
			MaxHealth.SetValue(model.Health);
			PowerUp = new AdjustableProperty<string>("PowerUp", this, true);
			PowerUp.OnChange += HandlePowerUpActivation;

			DamageUpDuration = new AdjustableProperty<float>("DamageUpDuration", this);
			SpeedUpDuration = new AdjustableProperty<float>("SpeedUpDuration", this);
			PowerUpMaxDuration = new AdjustableProperty<float>("PowerUpMaxDuration", this);
			DamageUpDuration.SetValue(0);
			SpeedUpDuration.SetValue(0);
			PowerUpMaxDuration.SetValue(30);

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

		private void HandlePowerUpActivation()
		{
			if (PowerUp.GetValue() == "Damage")
			{
				DamageUpDuration.SetValue(PowerUpMaxDuration.GetValue());
				Root.IntervalRunner.UnsubscribeFromInterval(ReducePowerUpDuration);
				Root.IntervalRunner.SubscribeToInterval(ReducePowerUpDuration, 1f, false);
			}
			else if (PowerUp.GetValue() == "Speed")
			{
				SpeedUpDuration.SetValue(PowerUpMaxDuration.GetValue());
				Root.IntervalRunner.UnsubscribeFromInterval(ReduceSpeedUpDuration);
				Root.IntervalRunner.SubscribeToInterval(ReduceSpeedUpDuration, 1f, false);
			}
		}

		private void ReducePowerUpDuration()
		{
			var newPowerUpDuration = DamageUpDuration.GetValue() - 1f;
			if (newPowerUpDuration > -1)
			{
				DamageUpDuration.SetValue(newPowerUpDuration);
			}
			else
			{
				Root.IntervalRunner.UnsubscribeFromInterval(ReducePowerUpDuration);
			}
		}

		private void ReduceSpeedUpDuration()
		{
			var newSpeedUpDuration = SpeedUpDuration.GetValue() - 1f;
			if (newSpeedUpDuration > -1)
			{
				SpeedUpDuration.SetValue(newSpeedUpDuration);
			}
			else
			{
				Root.IntervalRunner.UnsubscribeFromInterval(ReduceSpeedUpDuration);
			}
		}
	}
}