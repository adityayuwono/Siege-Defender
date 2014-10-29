using System;
using Scripts.Core;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class Player : Element
    {
        private readonly PlayerModel _model;

        public Player(PlayerModel model, Scene parent) : base(model, parent)
        {
            _model = model;

            Health = new AdjustableProperty<float>("Health", this);
            MaxHealth = new AdjustableProperty<float>("MaxHealth", this);
            MaxHealth.SetValue(_model.Health);

            foreach (var shooterModel in _model.Shooters)
                Elements.Add(new Shooter(shooterModel, this));
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
                if (OnGameOver != null)
                    OnGameOver();// Player's health is 0, this means game over :( (
        }

        public readonly AdjustableProperty<float> Health;
        public readonly AdjustableProperty<float> MaxHealth;

        public event Action OnGameOver;
    }
}
