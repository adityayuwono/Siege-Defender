﻿using Scripts.Components;
using Scripts.Components.SpecialEvents;
using Scripts.Helpers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Views
{
    public class ObjectView : BaseView
    {
	    protected Vector3 AssetScale;

        private readonly ViewModels.Object _viewModel;
        private readonly ObjectView _parent;
	    private bool _isLoaded;

		public ObjectView(ViewModels.Object viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;
        }

        private GameObject _gameObject;
        public GameObject GameObject
        {
            get
            {
                if (_gameObject == null)
                    throw new EngineException(this, "GameObject is null");

                return _gameObject;
            }
            private set { _gameObject = value; }
        }
        public Transform Transform;

        protected override void OnShow()
        {
			base.OnShow();

            if (!_isLoaded)
            {
                _isLoaded = true;
                OnLoad();
            }

            GameObject.SetActive(true);

            SetPosition();
        }

        private void Object_OnStartSpecialEvent()
        {
            var specialEventControllers = GameObject.GetComponents<BaseSpecialEventController>();
	        foreach (var specialEventController in specialEventControllers)
	        {
		        specialEventController.StartSpecialEvent(_viewModel.Root);
	        }
        }

        protected override void OnHide(string reason)
        {
            base.OnHide(reason);
            KillGameObject(reason);
        }

        protected virtual void OnLoad()
        {
            GameObject = GetGameObject();
            Transform = GameObject.transform;

            // Cache initial setting of the transforms
            AssetScale = Transform.localScale;

            GameObject.AddComponent<ViewModelController>().ViewModel = _viewModel;

            GameObject.transform.parent = GetParent();

            _viewModel.OnStartSpecialEvent += Object_OnStartSpecialEvent;
        }
        protected virtual Transform GetParent()
        {
            Transform parentTransform;
	        if (_parent == null || _parent.GameObject == null)
	        {
		        parentTransform = GameObject.Find("Context").transform;
	        }
	        else
	        {
		        parentTransform = _parent.GameObject.transform;
	        }

            return parentTransform;
        }

        protected virtual GameObject GetGameObject()
        {
            var gameObject =_viewModel.Root.ResourceManager.GetGameObject(_viewModel.AssetId);
            gameObject.name = string.Format("{0}({1})", _viewModel.AssetId, _viewModel.Id);
            return gameObject;
        }

        protected virtual void SetPosition()
        {
            if (_viewModel.RandomPositionManager != null)
            {
                var enemyManagerView = _viewModel.Root.GetView<RandomPositionManagerView>(_viewModel.RandomPositionManager);
                Transform.position = enemyManagerView.GetRandomSpawnPoint();
            }
            else
            {
                Transform.localPosition = _viewModel.Position;
            }
        }

        protected void SetRandomPosition()
        {
            if (_viewModel.RandomPositionManager != null)
            {
                var enemyManagerView = _viewModel.Root.GetView<RandomPositionManagerView>(_viewModel.RandomPositionManager);
                Transform.position = enemyManagerView.GetRandomSpawnPoint(false);
            }
        }

        protected T AttachController<T>() where T : BaseController
        {
            var controller = GameObject.AddComponent<T>();
            controller.Setup(_viewModel);

            return controller;
        }

        private void KillGameObject(string reason)
        {
            BalistaContext.Instance.IntervalRunner.SubscribeToInterval(OnDeath, _viewModel.DeathDelay, false);
        }
        
	    protected void OnDeath()
        {
	        if (BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(OnDeath) && _gameObject != null)
	        {
		        OnDeath(string.Format("{0}:{1}'s Death", GetType(), Id));
	        }
        }
        
	    protected virtual void OnDeath(string reason)
        {
            GameObject.SetActive(false);
            _viewModel.Deactivate(reason);
        }
        
	    protected override void OnDestroy()
        {
            _viewModel.OnStartSpecialEvent -= Object_OnStartSpecialEvent;

            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(OnDeath);

            _isLoaded = false;

            Transform = null;
            Object.DestroyImmediate(_gameObject);
            _gameObject = null;

            base.OnDestroy();
        }
    }
}
