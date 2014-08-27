﻿using System.Collections;
using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.ViewModels;
using Scripts.Views;
using UnityEngine;

namespace Scripts
{
    public class EngineBase : BaseViewModel
    {
        private EngineModel _model;
        
        public EngineBase(EngineModel model, ObjectViewModel parent) : base(model, parent)
        {
            _model = model;
        }

        public virtual void MapInjections()
        {
            
        }


        private readonly Dictionary<string, BaseView> _views = new Dictionary<string, BaseView>();
        public void RegisterView(BaseViewModel viewModel, BaseView view)
        {
            if (_views.ContainsKey(viewModel.Id))
                throw new EngineException(this, string.Format("Failed to register View of Type: {1}, duplicate for Id: {0}", viewModel.Id, viewModel.GetType()));

            _views.Add(viewModel.Id, view);
        }
        public T GetView<T>(string id) where T:BaseView
        {
            if (!_views.ContainsKey(id))
                throw new EngineException(this, string.Format("Failed to get view for Id: {0}", id));

            return _views[id] as T;
        }

        public IIoCContainer IoCContainer;
        public IResource ResourceManager;


        public override EngineBase Root
        {
            get { return this; }
        }

        public virtual ProjectileModel GetProjectileModel(string projectileId)
        {
            throw new System.NotImplementedException();
        }

        public virtual void DamageEnemy(string enemyId, float damage)
        {
            throw new System.NotImplementedException();
        }

        public ObjectModel GetObjectModel(string id)
        {
            foreach (var objectModel in _model.Objects)
            {
                if (objectModel.Id == id)
                    return objectModel;
            }
            throw new EngineException(this, string.Format("ObjectModel not found, Id: {0}", id));
        }

        public virtual EnemyBaseModel GetEnemy(string enemyId)
        {
            throw new System.NotImplementedException();
        }

        public virtual LevelModel GetLevel(string levelId)
        {
            throw new System.NotImplementedException();
        }

        public virtual void RegisterEnemy(EnemyBaseViewModel enemy)
        {
            throw new System.NotImplementedException();
        }

        public virtual void RemoveEnemy(ObjectViewModel enemy)
        {
            throw new System.NotImplementedException();
        }

        public virtual Coroutine StartCoroutine(IEnumerator coroutine)
        {
            throw new System.NotImplementedException();
        }
    }
}
