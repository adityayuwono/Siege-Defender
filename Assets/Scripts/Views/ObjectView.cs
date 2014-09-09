using System.Collections;
using Scripts.Components;
using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class ObjectView : BaseView
    {
        private readonly ObjectViewModel _viewModel;
        private readonly ObjectView _parent;

        public ObjectView(ObjectViewModel viewModel, ObjectView parent) : base(viewModel, parent)
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
                    throw new EngineException(this, string.Format("GameObject is null"));

                return _gameObject;
            }
            private set { _gameObject = value; }
        }
        public Transform Transform;


        protected override void OnShow()
        {
            if (!_isLoaded)
            {
                _isLoaded = true;
                OnLoad();
            }
            GameObject.SetActive(true);
            SetPosition();
        }

        protected override void OnHide(string reason)
        {
            base.OnHide(reason);
            
            KillGameObject(reason);
        }

        protected Vector3 _assetScale;
        protected Vector3 _assetRotation;
        private bool _isLoaded;
        protected virtual void OnLoad()
        {
            GameObject = GetGameObject();
            Transform = GameObject.transform;

            // Cache initial setting of the transforms
            _assetScale = Transform.localScale;
            _assetRotation = Transform.localEulerAngles;

            GameObject.AddComponent<ViewModelController>().ViewModel = _viewModel;

            GameObject.transform.parent = GetParent();
        }
        protected virtual Transform GetParent()
        {
            Transform parentTransform;
            if (_parent == null || _parent.GameObject == null)
                parentTransform = GameObject.Find("Context").transform;
            else
                parentTransform = _parent.GameObject.transform;

            return parentTransform;
        }
        protected virtual void OnDestroy() { }
        protected virtual GameObject GetGameObject()
        {
            var gameObject =_viewModel.Root.ResourceManager.GetGameObject(_viewModel.AssetId);
            gameObject.name = string.Format("{0}({1})", _viewModel.AssetId, _viewModel.Id);
            return gameObject;
        }
        protected virtual void SetPosition()
        {
            GameObject.transform.localPosition = _viewModel.Position;
        }



        protected T AttachController<T>() where T : BaseController
        {
            var controller = GameObject.AddComponent<T>();
            controller.Setup(_viewModel);

            return controller;
        }


        private void KillGameObject(string reason)
        {
            _viewModel.Root.StartCoroutine(DelayedDeath(_viewModel.DeathDelay, reason));
        }
        private IEnumerator DelayedDeath(float delay, string reason)
        {
            yield return new WaitForSeconds(delay);
            OnDeath(reason);
        }
        protected virtual void OnDeath(string reason)
        {
            GameObject.SetActive(false);
            _viewModel.Deactivate(reason);
        }
    }
}
