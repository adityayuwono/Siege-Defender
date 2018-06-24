using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
	/// <summary>
	///     Randomly sets itself to true, sounds dirty...
	///     The Value represent the percentage for success
	/// </summary>
	public class RandomCondition : BaseCondition
	{
		private readonly RandomConditionModel _model;

		private readonly Property<double> _randomizedValue = new Property<double>(true);

		private readonly double _threshold;

		public RandomCondition(RandomConditionModel model, Base parent) : base(model, parent)
		{
			_model = model;

			double valueAsDouble;
			if (double.TryParse(_model.Value, out valueAsDouble))
			{
				if (valueAsDouble > 100)
				{
					throw new EngineException(this,
						"Value provided should be a percentage of success between 0-100, other than those is kinda pointless");
				}

				_threshold = valueAsDouble / 100d;
			}
			else
			{
				throw new EngineException(this, "Value provided is not a double, it should be only number like: 50 or 3.141592");
			}
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			_randomizedValue.OnChange += Value_OnChange;

			if (_model.Frequency > 0)
			{
				Root.IntervalRunner.SubscribeToInterval(Randomize, _model.Frequency, false);
			}
			else
			{
				Randomize();
			}
		}

		protected override void OnDeactivate()
		{
			_randomizedValue.OnChange -= Value_OnChange;

			if (_model.Frequency > 0)
			{
				Root.IntervalRunner.UnsubscribeFromInterval(Randomize);
			}

			base.OnDeactivate();
		}

		protected override object FindTarget(string targetPath)
		{
			return _randomizedValue;
		}
		
		private void Value_OnChange()
		{
			IsMatch.SetValue(_randomizedValue.GetValue() <= _threshold);
		}

		private void Randomize()
		{
			var randomValue = Root.Randomizer.NextDouble();
			_randomizedValue.SetValue(randomValue);
		}
	}
}