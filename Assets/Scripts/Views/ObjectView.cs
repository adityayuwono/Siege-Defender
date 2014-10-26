using Scripts.Components;
using Scripts.Helpers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Views
{
    public class ObjectView : BaseView
    {
        private readonly ViewModels.Object _viewModel;
        private readonly ObjectView _parent;

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

        protected Vector3 AssetScale;
        private bool _isLoaded;
        protected virtual void OnLoad()
        {
            GameObject = GetGameObject();
            Transform = GameObject.transform;

            // Cache initial setting of the transforms
            AssetScale = Transform.localScale;

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
        private void OnDeath()
        {
            if (BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(OnDeath) && _gameObject != null)
                OnDeath(string.Format("{0}:{1}'s Death", GetType(), Id));
        }
        protected virtual void OnDeath(string reason)
        {
            GameObject.SetActive(false);
            _viewModel.Deactivate(reason);
        }
        protected override void OnDestroy()
        {
            BalistaContext.Instance.IntervalRunner.UnsubscribeFromInterval(OnDeath);

            _isLoaded = false;

            Transform = null;
            Object.DestroyImmediate(_gameObject);
            _gameObject = null;

            base.OnDestroy();
        }
    }
}
