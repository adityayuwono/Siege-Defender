using System;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.ViewModels;

namespace Scripts.Core
{
    public class AdjustableProperty<T> : Property<T> , IDisposable
    {
        private readonly string _propertyId;
        private readonly IContext _context;
        private readonly Base _viewModel;
        public AdjustableProperty(string propertyId, Base baseViewModel, bool isAlwaysChanging = false) : base (isAlwaysChanging)
        {
            _propertyId = propertyId;
            _viewModel = baseViewModel;
            _context = baseViewModel as IContext ?? baseViewModel.GetParent<IContext>();

            if (_context == null)
                throw new EngineException(baseViewModel, "Failed to find Context in the hierarchy");

            _context.PropertyLookup.RegisterProperty(baseViewModel, _propertyId, this);
        }

        public void Dispose()
        {
            _context.PropertyLookup.UnregisterProperty(_viewModel, _propertyId);
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

            InvokeChangedEvent();
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

        protected void InvokeChangedEvent()
        {
            if (OnChange != null)
                OnChange();
        }

        public event Action OnChange;
    }

    public interface IChangeProperty
    {
        event Action OnChange;
    }
}
