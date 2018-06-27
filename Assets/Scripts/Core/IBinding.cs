using System;

namespace Scripts.Core
{
	public interface IBinding
	{
		Property GetProperty();
		Property<T> GetPropertyAs<T>();
		object GetValue();
		void Bind(Action updateHandler);
		void Unbind();
		object Get();
	}
}
