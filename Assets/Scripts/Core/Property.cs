using System;
using Scripts.ViewModels;

namespace Scripts.Core
{
    public class AdjustableProperty<T> : Property<T> , IDisposable
    {
        private readonly string _propertyId;
        private readonly Base _viewModel;
        public AdjustableProperty(string propertyId, Base viewModel, bool isAlwaysChanging = false) : base (isAlwaysChanging)
        {
            _propertyId = propertyId;
            _viewModel = viewModel;

            viewModel.Root.RegisterProperty(_viewModel, _propertyId, this);
        }

        public void Dispose()
        {
            _viewModel.Root.UnregisterProperty(_viewModel, _propertyId);
        }
    }

    public class Property<T> : Property
    {
        private readonly bool _isAlwaysChanging;
        public Property(bool isAlwaysChanging = false)
        {
            _isAlwaysChanging = isAlwaysChanging;
            _value = default(T);
        }

        public void SetValue(T newValue)
        {
            if (_value!=null && _value.Equals(newValue) && !_isAlwaysChanging) 
                return;
            
            _value = newValue;
            
            if (OnChange!=null)
                OnChange();
        }

        public new T GetValue()
        {
            return _value != null ? (T) _value : default(T);
        }
    }

    public class Property : IChangeProperty
    {
        protected object _value;
        public object GetValue()
        {
            return _value;
        }
        public Action OnChange { get; set; }
    }

    public interface IChangeProperty
    {
        Action OnChange { get; set; }
    }
}
