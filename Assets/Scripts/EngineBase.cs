﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.ViewModels;
using Scripts.ViewModels.GUIs;
using Scripts.Views;
using UnityEngine;

namespace Scripts
{
    public class EngineBase : BaseViewModel
    {
        private readonly Engine_Model _model;
        
        public EngineBase(Engine_Model model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public virtual void MapInjections() { }


        private readonly Dictionary<string, BaseView> _views = new Dictionary<string, BaseView>();
        public void RegisterView(BaseViewModel viewModel, BaseView view)
        {
            if (_views.ContainsKey(viewModel.Id))
                throw new EngineException(this, string.Format("Failed to register View of Type: {1}, duplicate for Id: {0}", viewModel.Id, viewModel.GetType()));

            _views.Add(viewModel.Id, view);
        }
        public void UnregisterView(BaseViewModel viewModel)
        {
            _views.Remove(viewModel.Id);
        }
        public T GetView<T>(BaseViewModel viewModel) where T:BaseView
        {
            var id = viewModel.Id;
            if (!_views.ContainsKey(id))
                throw new EngineException(this, string.Format("Failed to get view for Id: {0}", id));

            return _views[id] as T;
        }


        public IIoCContainer IoCContainer;
        public BindingManager Binding;
        public IResource ResourceManager;

        public DamageDisplayManager DamageDisplay;



        public override EngineBase Root
        {
            get { return this; }
        }

        public Object_Model GetObjectModel(string id)
        {
            foreach (var objectModel in _model.Objects.Where(objectModel => objectModel.Id == id))
            {
                return objectModel;
            }
            throw new EngineException(this, string.Format("ObjectModel not found, Id: {0}", id));
        }

        #region Property Lookup
        private readonly Dictionary<string, Dictionary<string, Property>> _properties = new Dictionary<string, Dictionary<string, Property>>(); 
        public void RegisterProperty(BaseViewModel viewModel, string id, Property property)
        {
            if (_properties.ContainsKey(id))
            {
                // We already register that type of property, let's add to the list
                var propertyDict = _properties[id];
                if (propertyDict.ContainsKey(viewModel.Id))
                    // Woops, duplicate
                    throw new EngineException(this, string.Format("Failed to register property: {0} for ViewModel: {1}, Duplicate is found", id, viewModel.Id));

                // OK, everything is good, add a new property
                propertyDict.Add(viewModel.Id, property);
            }
            else
            {
                // No similar property registered yet, meaning this is the first of it's kind, momentous
                _properties.Add(id, new Dictionary<string, Property> {{viewModel.Id, property}});
            }
        }

        public void UnregisterProperty(BaseViewModel viewModel, string id)
        {
            _properties[id].Remove(viewModel.Id);
        }

        /// <summary>
        /// Get Property from list, will throw exception upon failure
        /// </summary>
        /// <returns>The Property asked, Does not return null</returns>
        public Property GetProperty(string viewModelId, string propertyId)
        {
            if (!_properties.ContainsKey(propertyId))
                throw new EngineException(this, string.Format("Failed to find Property with Id: {0} of ViewModel: {1}, the property is not registered", propertyId, viewModelId));

            var propertyDict = _properties[propertyId];
            if (!propertyDict.ContainsKey(viewModelId))
                throw new EngineException(this, string.Format("Failed to find Property with Id: {0} of ViewModel: {1}", propertyId, viewModelId));

            return propertyDict[viewModelId];
        }
        #endregion

        #region Virtual Methods
        public virtual Inventory_Model InventoryModel
        {
            get { throw new System.NotImplementedException(); }
        }

        public virtual void Save()
        {
            throw new System.NotImplementedException();
        }

        public virtual Level_Model GetLevel(string levelId)
        {
            throw new System.NotImplementedException();
        }

        public virtual Coroutine StartCoroutine(IEnumerator coroutine)
        {
            throw new System.NotImplementedException();
        }

        public virtual void ThrowError(string message)
        {
            throw new System.NotImplementedException();
        }

        public virtual void ChangeScene(string sceneId)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
