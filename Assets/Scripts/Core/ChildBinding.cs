using System;
using Scripts.ViewModels;

namespace Scripts.Core
{
	public class ChildBinding : IBinding
	{
		private readonly Base _child;
		public ChildBinding(Base child)
		{
			_child = child;
		}

		public Property GetProperty()
		{
			throw new NotImplementedException();
		}

		public Property<T> GetPropertyAs<T>()
		{
			throw new NotImplementedException();
		}

		public object GetValue()
		{
			return _child;
		}

		public void Bind(Action updateHandler)
		{
			throw new NotImplementedException();
		}

		public void Unbind()
		{
			throw new NotImplementedException();
		}

		public object Get()
		{
			return GetValue();
		}
	}
}
