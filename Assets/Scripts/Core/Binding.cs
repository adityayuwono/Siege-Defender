using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Interfaces;

namespace Scripts.Core
{
	public class Binding
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

			UpdateOnChangeBinding();
		}

		public void Bind(Action updateText)
		{
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
			var newBinding = _context.PropertyLookup.GetBinding("{" + string.Join(".", _bindingPaths.ToArray()) + "}");
			_properties = newBinding._properties;
			newBinding.UnbindFromOnChange();
			UpdateOnChangeBinding();
			_onChange();
		}

		public object GetValue()
		{
			return _properties.Last().GetValue();
		}
	}
}
