using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Interfaces;

namespace Scripts.Core
{
	public class Binding : IBinding
	{
		private Action _onChange;
		private readonly IContext _context;
		private List<Property> _properties;
		private readonly List<string> _bindingPaths;

		public Binding(IContext context, List<Property> properties, List<string> bindingPaths)
		{
			_context = context;
			_properties = properties;
			_bindingPaths = bindingPaths;
		}

		public void Bind(Action updateText)
		{
			UpdateOnChangeBinding();
			_onChange = updateText;
		}

		public void Unbind()
		{
			UnbindFromOnChange();
			_onChange = null;
		}

		private void UpdateOnChangeBinding()
		{
			foreach (var property in _properties)
			{
				property.OnChange += UpdateBinding;
			}
		}

		private void UnbindFromOnChange()
		{
			foreach (var property in _properties)
			{
				property.OnChange -= UpdateBinding;
			}
		}

		private void UpdateBinding()
		{
			UnbindFromOnChange();
			var newBinding = _context.PropertyLookup.GetBinding("{" + string.Join(".", _bindingPaths.ToArray()) + "}") as Binding;
			_properties = newBinding._properties;
			UpdateOnChangeBinding();
			_onChange();
		}

		public object Get()
		{
			if (_properties.Count == 0)
			{
				return _context;
			}
			return GetProperty();
		}

		public Property GetProperty()
		{
			return _properties.Last();
		}

		public Property<T> GetPropertyAs<T>()
		{
			return GetProperty() as Property<T>;
		}

		public object GetValue()
		{
			if (_properties.Count == 0)
			{
				return "";
			}
			return _properties.Last().GetValue();
		}
	}
}
