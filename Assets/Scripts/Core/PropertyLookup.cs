using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Contexts;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.ViewModels;
using Scripts.ViewModels.Items;

namespace Scripts.Core
{
	public class PropertyLookup
	{
		private readonly Dictionary<string, Base> _children = new Dictionary<string, Base>();
		private readonly IContext _context;
		private readonly Dictionary<string, IContext> _contexts = new Dictionary<string, IContext>();
		private readonly IRoot _engine;

		private readonly Dictionary<string, Dictionary<string, Property>> _properties =
			new Dictionary<string, Dictionary<string, Property>>();

		public PropertyLookup(IRoot engine, IContext context)
		{
			_engine = engine;
			_context = context;
			_contexts.Add("This", context);

			if (engine != context)
			{
				_engine.PropertyLookup.RegisterContext(context);
			}
		}

		private void RegisterContext(IContext context)
		{
			if (_contexts.ContainsKey(context.Id))
			{
				throw new Exception(string.Format("Failed to register {0} to {1}, a duplicate is found", context.Id, _context.Id));
			}

			_contexts.Add(context.Id, context);
		}

		public IContext GetContext(string contextId)
		{
			if (!_contexts.ContainsKey(contextId))
			{
				return null;
			}

			return _contexts[contextId];
		}

		public Base GetChild(string childId)
		{
			if (!_children.ContainsKey(childId))
			{
				return null;
			}
			return _children[childId];
		}

		public void RegisterProperty(Base viewModel, string propertyId, Property property)
		{
			var viewModelId = viewModel == _context ? "This" : viewModel.Id;

			if (!_children.ContainsKey(viewModelId))
			{
				_children.Add(viewModelId, viewModel);
			}

			if (_properties.ContainsKey(propertyId))
			{
				// We already register that type of property, let's add to the list
				var propertyDict = _properties[propertyId];
				if (propertyDict.ContainsKey(viewModelId))
				{
					throw new EngineException(_engine,
						string.Format("PropertyLookup: Failed to register property: {0} for ViewModel: {1}, Duplicate is found",
							propertyId, viewModel.Id));
				}

				// OK, everything is good, add a new property
				propertyDict.Add(viewModelId, property);
			}
			else
			{
				// No similar property registered yet, meaning this is the first of it's kind, momentous
				_properties.Add(propertyId, new Dictionary<string, Property> { { viewModelId, property } });
			}
		}

		public void UnregisterProperty(Base viewModel, string id)
		{
			_properties[id].Remove(viewModel.Id);
		}

		#region Get Property

		/// <summary>
		///     Get Property from list, will throw exception upon failure
		/// </summary>
		/// <returns>The Property asked, Does not return null</returns>
		private Property GetProperty(string viewModelId, string propertyId)
		{
			// This is the last context, we simply get the property
			if (!_properties.ContainsKey(propertyId))
			{
				return null;
			}

			// We got the properties, return the property owned by the viewmodel that we want
			var propertyDict = _properties[propertyId];
			if (!propertyDict.ContainsKey(viewModelId))
			{
				return null;
			}

			// This means we found the property, return it
			return propertyDict[viewModelId];
		}

		/// <summary>
		///     Go through the list on the Main Engine, then get the property needed
		/// </summary>
		/// <param name="path">string </param>
		/// <returns>May return null</returns>
		public IBinding GetBinding(string path)
		{
			if (path.StartsWith("{") && path.EndsWith("}"))
			{
				path = path.Replace("{", "").Replace("}", "");

				var paths = path.Split('.');

				var context = _context;
				var foundPath = String.Empty;
				var properties = new List<Property>();
				var bindingPaths = new List<string>();
				for (var i = 0; i < paths.Length; i++)
				{
					var currentPath = paths[i];
					if (!string.IsNullOrEmpty(foundPath))
					{
						currentPath = foundPath;
						foundPath = String.Empty;
					}

					if (paths.Length == 1)
					{
						return new Binding(context, properties, bindingPaths);
					}

					if (currentPath == "Root")
					{
						context = _engine;
						continue;
					}

					if (currentPath == "DataRoot")
					{
						context = DataContext.Instance;
						continue;
					}

					var isLast = i == paths.Length - 1;
					if (!isLast)
					{
						// As long as it's not the last one, keep iterating through context
						var newContext = context.PropertyLookup.GetContext(currentPath);
						if (newContext != null)
						{
							context = newContext;
							continue;
						}

						var isOneBeforeLast = i + 1 == paths.Length - 1;
						if (isOneBeforeLast)
						{
							var foundProperty = context.PropertyLookup.GetProperty(currentPath, paths[i + 1]);
							if (foundProperty != null)
							{
								properties.Add(foundProperty);
								bindingPaths.Add(paths[i + 1]);
								return new Binding(context, properties, bindingPaths);
							}
						}

						var contextProperty = context.PropertyLookup.GetProperty(currentPath, paths[i + 1]);
						if (contextProperty != null)
						{
							var tryBase = (contextProperty.GetValue() as Base);
							if (tryBase != null)
							{
								properties.Add(contextProperty);
								bindingPaths.Add(currentPath);
								bindingPaths.Add(paths[i + 1]);
								foundPath = tryBase.Id;
								continue;
							}
						}
						else
						{
							contextProperty = context.PropertyLookup.GetProperty("This", currentPath);
							if (contextProperty != null)
							{
								var tryBase = (contextProperty.GetValue() as Base);
								if (tryBase != null)
								{
									properties.Add(contextProperty);
									bindingPaths.Add(currentPath);
									bindingPaths.Add(paths[i + 1]);
									foundPath = tryBase.Id;
									continue;
								}
								else
								{
									properties.Add(contextProperty);
									bindingPaths.Add(currentPath);
									bindingPaths.Add(paths[i + 1]);
									return new Binding(context, properties, bindingPaths);
								}
							}
						}
					}
					else
					{
						var foundContext = context.PropertyLookup.GetContext(currentPath);
						if (foundContext != null)
						{
							return new Binding(foundContext, properties, bindingPaths);
						}

						var foundProperty = context.PropertyLookup.GetProperty("This", currentPath);
						if (foundProperty != null)
						{
							properties.Add(foundProperty);
							bindingPaths.Add(currentPath);
							return new Binding(context, properties, bindingPaths);
						}

						foundProperty = context.PropertyLookup.GetProperty(currentPath, paths[i]);
						if (foundProperty != null)
						{
							properties.Add(foundProperty);
							bindingPaths.Add(paths[i]);
							return new Binding(context, properties, bindingPaths);
						}

						var foundChild = context.PropertyLookup.GetChild(currentPath);
						if (foundChild != null)
						{
							return new ChildBinding(foundChild);
						}
					}
				}
			}

			// Yeah... nothing
			return null;
		}

		#endregion

		public void Detach(Base item)
		{
			foreach (var property in _properties.ToArray())
			{
				var propertyDictionary = property.Value;
				if (propertyDictionary.ContainsKey(item.Id))
				{
					propertyDictionary.Remove(item.Id);
				}
			}
		}

		public void Attach(Base item)
		{
			foreach (var itemProperty in item.Properties)
			{
				RegisterProperty(item, itemProperty.PropertyId, itemProperty);
			}
		}
	}
}