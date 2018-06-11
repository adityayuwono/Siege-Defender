using Scripts.Components;
using Scripts.Components.SpecialEvents;
using Scripts.Extensions;
using Scripts.Helpers;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Views
{
	public class ObjectView : BaseView
	{
		private readonly ObjectView _parent;

		private readonly Object _viewModel;

		private GameObject _gameObject;
		private bool _isLoaded;
		protected Vector3 AssetScale;

		public ObjectView(Object viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
			_parent = parent;
		}

		public GameObject GameObject
		{
			get
			{
				if (_gameObject == null)
				{
					throw new EngineException(this, "GameObject is null");
				}

				return _gameObject;
			}
			private set { _gameObject = value; }
		}

		public Transform Transform { get; private set; }

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

			var parent = GetParent();
			if (parent != null)
			{
				GameObject.transform.SetParent(parent, false);
			}

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
			var tryFindChild = _parent.Transform.FindChildRecursivelyBreadthFirst(_viewModel.AssetId);
			if (tryFindChild != null)
			{
				return tryFindChild.gameObject;
			}

			var gameObject = _viewModel.Root.ResourceManager.GetGameObject(_viewModel.AssetId);
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

		protected void OnDeath()
		{
			if (_viewModel.Root.Context.IntervalRunner.UnsubscribeFromInterval(OnDeath) && _gameObject != null)
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

			_viewModel.Root.Context.IntervalRunner.UnsubscribeFromInterval(OnDeath);

			_isLoaded = false;

			Transform = null;
			UnityEngine.Object.DestroyImmediate(_gameObject);
			_gameObject = null;

			base.OnDestroy();
		}

		private void Object_OnStartSpecialEvent()
		{
			var specialEventControllers = GameObject.GetComponents<BaseSpecialEventController>();
			foreach (var specialEventController in specialEventControllers)
			{
				specialEventController.StartSpecialEvent(_viewModel.Root);
			}
		}

		private void KillGameObject(string reason)
		{
			_viewModel.Root.Context.IntervalRunner.SubscribeToInterval(OnDeath, _viewModel.DeathDelay, false);
		}
	}
}