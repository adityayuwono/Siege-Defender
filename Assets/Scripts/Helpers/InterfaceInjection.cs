// Version 1.2
// 8/4/2014

using System;
using System.Collections.Generic;

namespace Scripts.Helpers
{
	public interface IIoCContainer
	{
		InterfaceContainer RegisterFor<T>();

		/// <summary>
		///     Create a new instance bound to the specified type of key
		/// </summary>
		/// <typeparam name="T">Interface type of key</typeparam>
		/// <param name="classType"></param>
		/// <param name="parameters"></param>
		/// <returns>New instance of type bound to key</returns>
		T GetInstance<T>(Type classType, object[] parameters = null) where T : class;
	}

	/// <summary>
	///     Container for keeping all map from interface to a concrete class
	/// </summary>
	public class IoCContainer : BaseContainer<InterfaceContainer>, IIoCContainer
	{
		// Cached all dependency by type reference
		public InterfaceContainer RegisterFor<T>()
		{
			return Add<T>();
		}

		/// <summary>
		///     Create a new instance bound to the specified type of key
		/// </summary>
		/// <typeparam name="T">Interface type of key</typeparam>
		/// <param name="classType"></param>
		/// <param name="parameters"></param>
		/// <exception cref="Exception"></exception>
		/// <returns>New instance of type bound to key</returns>
		public T GetInstance<T>(Type classType, object[] parameters = null) where T : class
		{
			// The type we want it to be
			var key = typeof(T);

			// Check if we have mapped the key type
			var interfaceContainer = Get(classType);
			if (interfaceContainer == null)
			{
				return default(T);
			}

			var instanceContainer = interfaceContainer.GetInstance(key);
			if (instanceContainer != null)
			{
				var instanceObject = instanceContainer.GetInstance();
				if (instanceObject != null)
				{
					return instanceObject as T;
				}
			}

			var instanceType = interfaceContainer.GetInstanceType(key);

			if (instanceType == null)
			{
				return default(T);
			}

			// We have the instance type that we want, now we activate the constructor
			var instance = CreateInstance(instanceType, parameters);
			if (instance == null)
			{
				throw new Exception(string.Format("Failed to create an Instance for {0} with parameters: {1}", key, parameters));
			}

			return instance as T;
		}

		private static object CreateInstance(Type instanceType, object[] parameters)
		{
			object instance;
			try
			{
				instance = Activator.CreateInstance(instanceType, parameters);
			}
			catch (Exception ex)
			{
				// Go through extra effort to make sure we provide enough help
				var possibleConstructors = "(";
				var constructors = instanceType.GetConstructors();
				foreach (var constructorInfo in constructors)
				{
					foreach (var parameterInfo in constructorInfo.GetParameters())
					{
						possibleConstructors += parameterInfo.ParameterType + ", ";
					}
					possibleConstructors += ")\n";
				}

				throw new Exception(string.Format(
					"{0}\nCheck the inheritance or encapsulation of the constructor\nPossible Parameters are:\n{1}", ex,
					possibleConstructors));
			}

			return instance;
		}
	}

	public class InterfaceContainer : BaseContainer<InstanceContainer>
	{
		public InstanceContainer TypeOf<T>()
		{
			return Add<T>();
		}

		public Type GetInstanceType(Type key)
		{
			return Get(key) != null ? Get(key).Type : null;
		}

		public InstanceContainer GetInstance(Type key)
		{
			var instanceContainer = Get(key);
			return instanceContainer;
		}
	}

	public class InstanceContainer : IInjection
	{
		private object _object;
		private Type _type;

		public Type Type
		{
			get { return _type; }
			private set
			{
				if (_type != null)
				{
					throw new Exception(string.Format("Failed to bind to type: {0}, type is already bound to type: {1}", Type, value));
				}

				_type = value;
			}
		}

		public void To<T>()
		{
			Type = typeof(T);
		}

		public void ToNull()
		{
			Type = null;
		}

		public void To(object target)
		{
			_object = target;
		}

		public object GetInstance()
		{
			return _object;
		}
	}

	public class BaseContainer<T> : IInjection where T : class, IInjection, new()
	{
		private readonly Dictionary<Type, IInjection> _maps = new Dictionary<Type, IInjection>();

		protected T Add<TInjection>()
		{
			var interfaceType = typeof(TInjection);

			if (!_maps.ContainsKey(interfaceType))
			{
				_maps.Add(interfaceType, new T());
			}

			return _maps[interfaceType] as T;
		}

		protected T Get(Type key)
		{
			if (!_maps.ContainsKey(key))
			{
				return default(T);
			}

			return _maps[key] as T;
		}
	}

	public interface IInjection
	{
	}
}