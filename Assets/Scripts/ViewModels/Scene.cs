using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.Models.Levels;

namespace Scripts.ViewModels
{
	public class Scene : Interval<Object>, IContext
	{
		public readonly AdjustableProperty<bool> IsLoadingInProgress;
		private string _levelId;
		private PropertyLookup _propertyLookup;

		public EnemyManager EnemyManager;

		public Scene(SceneModel model, Base parent) : base(model, parent)
		{
			IsLoadingInProgress = new AdjustableProperty<bool>("IsLoadingInProgress", this);
		}

		public PropertyLookup PropertyLookup
		{
			get
			{
				if (_propertyLookup == null)
				{
					_propertyLookup = new PropertyLookup(Root, this);
				}

				return _propertyLookup;
			}
		}

		public void Activate(string levelId)
		{
			_levelId = levelId;
			Activate();
		}

		public override void Show()
		{
			base.Show();

			if (EnemyManager == null && !string.IsNullOrEmpty(_levelId))
			{
				throw new EngineException(this,
					"No EnemyManager provided but there\'s a LevelId specified, there may have been a mistake");
			}

			if (EnemyManager != null && !string.IsNullOrEmpty(_levelId))
			{
				var levelModel = SDRoot.GetLevel(_levelId);

				// Start caching all Objects here
				StartCaching(levelModel.CacheList);
			}
		}

		private void StartCaching(List<SpawnModel> objectsToCache)
		{
			IsLoadingInProgress.SetValue(true);
			var objectIds = new List<string>();
			foreach (var spawnModel in objectsToCache)
			{
				objectIds.Add(spawnModel.EnemyId);
			}

			Root.StartCoroutine(CacheObjects(objectIds));
		}

		private IEnumerator CacheObjects(List<string> objectsToCache)
		{
			var activeObjects = new List<Object>();
			foreach (var objectId in objectsToCache)
			{
				var objectToCache = GetObject<Object>(objectId, null, this);
				objectToCache.Activate();
				objectToCache.Show(); // Have to show to cache, in order to prepare the prefabs
				objectToCache.TriggerIgnoreDelays();
				activeObjects.Add(objectToCache);
				yield return null;
			}

			ActiveObjects.OnChange += CheckIfAllObjectsAreDeactivated;

			foreach (var activeObject in activeObjects)
			{
				activeObject.Hide("Just for caching");
			}
		}

		private void CheckIfAllObjectsAreDeactivated()
		{
			var isAllObjectsAreDeactivated = ActiveObjects.GetValue() == 0;
			if (isAllObjectsAreDeactivated)
			{
				IsLoadingInProgress.SetValue(false);
				ActuallyShowTheScene();
			}
		}

		private void ActuallyShowTheScene()
		{
			EnemyManager.ActiveObjects.OnChange += ActiveObjects_OnChange;

			EnemyManager.Level.SetValue(_levelId);
		}

		private void ActiveObjects_OnChange()
		{
		}
	}
}