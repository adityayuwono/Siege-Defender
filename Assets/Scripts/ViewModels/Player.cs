using System;
using Scripts.Core;
using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class Player : Element
    {
        private readonly PlayerModel _model;

        public Player(PlayerModel model, Scene parent) : base(model, parent)
        {
            _model = model;

            Health = new AdjustableProperty<float>("Health", this);

            foreach (var shooterModel in _model.Shooters)
                Elements.Add(new Shooter(shooterModel, this));
        }

        protected override void OnActivate()
        {
            Health.SetValue(_model.Health);

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
            Debug.LogError(currentHealth);
            if (currentHealth <= 0)
                if (OnGameOver != null)
                    OnGameOver();// Player's health is 0, this means game over :(

        }

        public readonly AdjustableProperty<float> Health;

        public event Action OnGameOver;
    }
}
