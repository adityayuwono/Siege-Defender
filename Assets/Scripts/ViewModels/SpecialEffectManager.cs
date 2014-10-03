﻿using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class SpecialEffectManager : Interval<SpecialEffect>
    {
        private readonly SpecialEffectManagerModel _model;
        public SpecialEffectManager(SpecialEffectManagerModel model, Object parent) : base(model, parent)
        {
            _model = model;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            Root.SpecialEffectManager = this;
        }

        public void DisplaySpecialEffect(string id, Vector3 position)
        {
            var specialEffect = GetObject<SpecialEffect>(id);
            specialEffect.ShowSpecialEffect(position);
        }
    }
}