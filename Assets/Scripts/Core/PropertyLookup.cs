using System;
using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.ViewModels;

namespace Scripts.Core
{
    public class PropertyLookup
    {
        private readonly EngineBase _engine;
        private readonly IContext _context;
        public PropertyLookup(EngineBase engine, IContext context)
        {
            _engine = engine;
            _context = context;

            if (engine != context)
                _engine.PropertyLookup.RegisterContext(context);
        }

        private readonly Dictionary<string, IContext> _contexts = new Dictionary<string, IContext>();
        private void RegisterContext(IContext context)
        {
            if (_contexts.ContainsKey(context.Id))
                throw new Exception(string.Format("Failed to register {0} to {1}, a duplicate is found", context.Id, _context.Id));

            _contexts.Add(context.Id, context);
        }

        private IContext GetContext(string contextId)
        {
            if (!_contexts.ContainsKey(contextId))
                throw new EngineException(_context, string.Format("Failed to find context: {0}", contextId));

            return _contexts[contextId];
        }

        private readonly Dictionary<string, Dictionary<string, Property>> _properties = new Dictionary<string, Dictionary<string, Property>>();
        public void RegisterProperty(Base viewModel, string id, Property property)
        {
            if (_properties.ContainsKey(id))
            {
                // We already register that type of property, let's add to the list
                var propertyDict = _properties[id];
                if (propertyDict.ContainsKey(viewModel.Id))
                    // Woops, duplicate
                    throw new EngineException(_engine, string.Format("PropertyLookup: Failed to register property: {0} for ViewModel: {1}, Duplicate is found", id, viewModel.Id));

                // OK, everything is good, add a new property
                propertyDict.Add(viewModel.Id, property);
            }
            else
            {
                // No similar property registered yet, meaning this is the first of it's kind, momentous
                _properties.Add(id, new Dictionary<string, Property> { { viewModel.Id, property } });
            }
        }

        public void UnregisterProperty(Base viewModel, string id)
        {
            _properties[id].Remove(viewModel.Id);
        }

        #region Get Property
        /// <summary>
        /// Get Property from list, will throw exception upon failure
        /// </summary>
        /// <returns>The Property asked, Does not return null</returns>
        private Property GetProperty(string viewModelId, string propertyId)
        {
            // This is the last context, we simply get the property
            if (!_properties.ContainsKey(propertyId))
                throw new EngineException(_engine, string.Format("PropertyLookup: Failed to find Property with Id: '{0}' of ViewModel: '{1}', the property is not registered", propertyId, viewModelId));

            // We got the properties, return the property owned by the viewmodel that we want
            var propertyDict = _properties[propertyId];
            if (!propertyDict.ContainsKey(viewModelId))
                throw new EngineException(_engine, string.Format("PropertyLookup: Failed to find Property with Id: '{0}' of ViewModel: '{1}'", propertyId, viewModelId));

            // This means we found the property, return it
            return propertyDict[viewModelId];
        }

        /// <summary>
        /// Go through the list on the Main Engine, then get the property needed
        /// </summary>
        /// <param name="path">string </param>
        /// <returns>May return null</returns>
        public Property GetProperty(string path)
        {
            if (path.StartsWith("{") && path.EndsWith("}"))
            {
                path = path.Replace("{", "").Replace("}", "");
                var bindingPath = path.Split('.');

                if (bindingPath.Length == 2)
                {
                    var foundProperty = GetProperty(bindingPath[0], bindingPath[1]);
                    return foundProperty;
                }
                else
                {
                    if (bindingPath[0] == "Root")
                    {
                        var foundProperty = _engine.PropertyLookup.GetContext(bindingPath[1]).PropertyLookup.GetProperty(bindingPath[2], bindingPath[3]);
                        return foundProperty;
                    }
                }
            }

            return null;
        }

        public Property<T> GetProperty<T>(string path)
        {
            return GetProperty(path) as Property<T>;
        }
        #endregion
    }
}
