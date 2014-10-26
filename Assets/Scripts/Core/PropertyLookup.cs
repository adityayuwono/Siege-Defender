﻿using System;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Interfaces;

namespace Scripts.ViewModels
{
    public class PropertyLookup
    {
        private EngineBase _engine;
        private IContext _context;
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

        public IContext GetContext(string contextId)
        {
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

        /// <summary>
        /// Get Property from list, will throw exception upon failure
        /// </summary>
        /// <returns>The Property asked, Does not return null</returns>
        public Property GetProperty(string viewModelId, string propertyId)
        {
            // Check if it is a root override, no support for going to parent for now
            if (viewModelId.StartsWith(".."))
                return _engine.PropertyLookup.GetProperty(viewModelId.Replace("..", ""), propertyId);

            // Get the first in view model path
            var contextIds = viewModelId.Split('.');// The Context ids, split by .
            var contextId = contextIds[0];// The first context
            var isLastContext = contextIds.Length == 1;// If it is the only member of the array

            // If it is not the last view model, get it, and go to the next on the list
            if (!isLastContext)
            {
                // Check if that context does exist
                if (!_contexts.ContainsKey(contextId))
                    throw new EngineException(_engine, string.Format("PropertyLookup: Failed to find Property with Id: '{0}' of ViewModel: '{1}'", propertyId, viewModelId));

                // Found it, go to the next on the list
                return _contexts[contextId].PropertyLookup.GetProperty(viewModelId.Replace(contextId + ".", ""), propertyId);
            }
            
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
        private Property GetProperty(string path)
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
    }
}
