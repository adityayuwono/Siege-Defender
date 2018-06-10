using System;
using Scripts.Interfaces;

namespace Scripts.Core
{
	public class Property<T> : Property
	{
		private readonly bool _isAlwaysChanging;

		public Property(bool isAlwaysChanging = false)
		{
			_isAlwaysChanging = isAlwaysChanging;
			_value = default(T);
		}

		public virtual void SetValue(T newValue)
		{
			if (_value != null && _value.Equals(newValue) && !_isAlwaysChanging) return;

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

		public event Action OnChange;

		public object GetValue()
		{
			return _value;
		}

		protected void InvokeChangedEvent()
		{
			if (OnChange != null) OnChange();
		}
	}
}