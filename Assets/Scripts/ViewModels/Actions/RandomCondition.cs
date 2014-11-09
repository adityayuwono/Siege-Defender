using System;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    /// <summary>
    /// Randomly sets itself to true, sounds dirty...
    /// The Value represent the percentage for success
    /// </summary>
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

        protected override object FindTarget()
        {
            return _randomizedValue;
        }

        private readonly Random _randomizer = new Random();
        private readonly double _threshold;

        private void Randomize()
        {
            var randomValue = _randomizer.NextDouble();
            _randomizedValue.SetValue(randomValue);
        }

        protected override void Target_OnChanged()
        {
            IsMatch.SetValue(_randomizedValue.GetValue() <= _threshold);
            // Immediatelly set to false to allow 2 consecutive Matches when in luck, if there's such a thing in coding
            IsMatch.SetValue(false);
        }
    }
}
