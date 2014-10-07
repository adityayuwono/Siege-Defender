using System.Collections;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class Scene : Interval<Object>, IContext
    {
        private readonly SceneModel _model;

        public Scene(SceneModel model, Base parent) : base(model, parent)
        {
            _model = model;

            IsLoadingInProgress = new AdjustableProperty<bool>("IsLoadingInProgress", this);
        }

        public void Activate(string levelId)
        {
            _levelId = levelId;
            Activate();
        }

        public PropertyLookup PropertyLookup
        {
            get
            {
                if (_propertyLookup == null)
                    _propertyLookup = new PropertyLookup(Root, this);

                return _propertyLookup;
            } 
        }
        private PropertyLookup _propertyLookup;

        public AdjustableProperty<bool> IsLoadingInProgress;
        private string _levelId;
        public override void Show()
        {
            base.Show();

            if (EnemyManager == null && !string.IsNullOrEmpty(_levelId))
                throw new EngineException(this,
                    string.Format("You does not provide an EnemyManager yet there's a LevelId specified, there may have been a mistake"));

            if (EnemyManager != null && !string.IsNullOrEmpty(_levelId))
            {
                var levelModel = Root.GetLevel(_levelId);

                // Start caching all Objects here
                StartCaching(levelModel.CacheList);
            }
        }

        public void StartCaching(List<SpawnModel> objectsToCache)
        {
            IsLoadingInProgress.SetValue(true);
            var objectIds = new List<string>();
            foreach (var spawnModel in objectsToCache)
                objectIds.Add(spawnModel.EnemyId);

            Root.StartCoroutine(CacheObjects(objectIds));
        }

        private IEnumerator CacheObjects(List<string> objectsToCache)
        {
            ActiveObjects.OnChange += CheckIfAllObjectsAreDeactivated;

            foreach (var objectId in objectsToCache)
            {
                var enemy = GetObject<Object>(objectId, this);
                enemy.Activate();
                enemy.Show(); // Have to show to cache, in order to prepare the prefabs
                enemy.TriggerIgnoreDelays();
                enemy.Hide("Just for caching");
                yield return null;
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
            EnemyManager.Level.SetValue(_levelId);
        }

        public EnemyManager EnemyManager;
    }
}
