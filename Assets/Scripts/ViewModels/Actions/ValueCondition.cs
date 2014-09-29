using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
    public class ValueCondition : BaseCondition
    {
        private readonly ValueConditionModel _model;
        public ValueCondition(ValueConditionModel model, Base parent) : base(model, parent)
        {
            _model = model;

            // Parse for comparison sign
            _comparisonValue = _model.Value.Replace('\\', '<');
            var comparisonSign = _comparisonValue[0];
            if (comparisonSign != '<' && comparisonSign != '>' && comparisonSign != '!' && comparisonSign != '=')
                comparisonSign = '=';
            else
                _comparisonValue = _comparisonValue.Substring(1, _comparisonValue.Length - 1);

            _comparisonSign = comparisonSign;
        }

        private readonly char _comparisonSign;
        private readonly string _comparisonValue;

        protected override void Property_OnChange()
        {
            var isMatch = CompareProperty();
            IsMatch.SetValue(isMatch);
        }

        private bool CompareProperty()
        {
            var v1 = double.Parse(Property.GetValue().ToString());
            var v2 = double.Parse(_comparisonValue);

            switch (_comparisonSign)
            {
                case '<': return v1 < v2;
                case '>': return v1 > v2;
                case '=': return Property.GetValue().ToString() == _comparisonValue;
                case '!': return Property.GetValue().ToString() != _comparisonValue;
            }

            return false;
        }
    }
}
