using System;
using Scripts.Interfaces;

namespace Scripts.Core
{
	public class Property<T> : Property
	{
		private readonly bool _isAlwaysChanging;

		public Property(string propertyId, bool isAlwaysChanging = false) : base(propertyId)
		{
			_isAlwaysChanging = isAlwaysChanging;
			Value = default(T);
		}

		public virtual void SetValue(T newValue)
		{
			if (Value != null && Value.Equals(newValue) && !_isAlwaysChanging)
			{
				return;
			}

			Value = newValue;

			InvokeChangedEvent();
		}

		public new T GetValue()
		{
			return (T) Value;
		}
	}

	public class Property : IChangeProperty
	{
		public event Action OnChange;

		protected object Value;

		protected Property(string propertyId)
		{
			PropertyId = propertyId;
		}
		public string PropertyId { get; private set; }

		public object GetValue()
		{
			return Value;
		}

		protected void InvokeChangedEvent()
		{
			if (OnChange != null)
			{
				OnChange();
			}
		}
	}
}