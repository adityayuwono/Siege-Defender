using System;

namespace Scripts.Helpers
{
    public class Property<T>
    {
        public Action OnChange;


        private T _value;

        public void SetValue(T newValue)
        {
            if (_value.Equals(newValue)) return;
            
            _value = newValue;
            
            if (OnChange!=null)
                OnChange();
        }

        public T GetValue()
        {
            return _value;
        }
    }
}
