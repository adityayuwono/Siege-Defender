using System;

namespace Scripts.Core
{
    public class Property<T> : Property
    {
        public void SetValue(T newValue)
        {
            if (_value!=null && _value.Equals(newValue)) 
                return;
            
            _value = newValue;
            
            if (OnChange!=null)
                OnChange();
        }

        public T GetValue()
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
