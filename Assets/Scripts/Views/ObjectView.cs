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

            _viewModel.DoDestroy += DestroyGameObject;
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
            GameObject = GetGameObject();
            Transform = GameObject.transform;

            GameObject.AddComponent<ViewModelController>().ViewModel = _viewModel;
            
            Transform parentTransform = null;
            if (_parent == null || _parent.GameObject == null)
                parentTransform = GameObject.Find("Context").transform;
            else
                parentTransform = _parent.GameObject.transform;

            
            GameObject.transform.parent = parentTransform;
            SetPosition();
        }

        protected virtual GameObject GetGameObject()
        {
            var gameObject =_viewModel.Root.ResourceManager.GetGameObject(_viewModel.AssetId);
            gameObject.name += "_" + _viewModel.Id;
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



        private IEnumerator DelayedDeath(float delay)
        {
            yield return new WaitForSeconds(delay);
            Object.Destroy(GameObject);
            GameObject = null;
        }
        private void DestroyGameObject(ObjectViewModel viewModel)
        {
            OnDestroyGameObject();
        }
        protected virtual void OnDestroyGameObject()
        {
            _viewModel.Root.StartCoroutine(DelayedDeath(_viewModel.DeathDelay));
        }
    }
}
