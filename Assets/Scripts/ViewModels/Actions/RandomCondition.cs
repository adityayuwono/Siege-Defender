﻿using System;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class RandomCondition : BaseCondition
    {
        private readonly RandomConditionModel _model;
        public RandomCondition(RandomConditionModel model, Base parent) : base(model, parent)
        {
            _model = model;

            double valueAsDouble;
            if (double.TryParse(_model.Value, out valueAsDouble))
            {
                if (valueAsDouble > 100)
                    throw new EngineException(this, string.Format("Value provided should be a percentage of success between 0-100, other than those is kinda pointless"));

                _threshold = valueAsDouble/100d;
            }
            else
                throw new EngineException(this, string.Format("Value provided is not a double, it should be only number like: 50 or 3.141592"));
        }

        private readonly Property<double> _randomizedValue = new Property<double>();

        protected override void OnActivate()
        {
            base.OnActivate();

            Root.IntervalRunner.SubscribeToInterval(Randomize, _model.Frequency, false);
        }

        protected override void OnDeactivate()
        {
            Root.IntervalRunner.UnsubscribeFromInterval(Randomize);

            base.OnDeactivate();
        }

        protected override Property FindProperty()
        {
            return _randomizedValue;
        }

        private static readonly Random Randomizer = new Random();
        private readonly double _threshold;

        private void Randomize()
        {
            var randomValue = Randomizer.NextDouble();
            _randomizedValue.SetValue(randomValue);
        }

        protected override void Property_OnChange()
        {
            IsMatch.SetValue(_randomizedValue.GetValue() <= _threshold);
        }
    }
}