using System;
using System.Collections.Generic;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class Interval<T> : IntervalBase where T : Object
    {
        private readonly IntervalModel _model;

        protected Interval(IntervalModel model, Base parent) : base(model, parent)
        {
            _model = model;

            ActiveObjects = new AdjustableProperty<int>("ActiveObjects", this);
            ActiveObjects.SetValue(0);

            Interval.SetValue(_model.Interval);
        }

        public override void Hide(string reason)
        {
            Hide(reason, true);
        }

        protected void Hide(string reason, bool hideChildren)
        {
            if (hideChildren)
            {
                foreach (var activeObject in _activeObjects)
                {
                    activeObject.Hide(reason);
                }
            }

            base.Hide(reason);
        }

        #region Spawning Objects

        protected TU GetObject<TU>(string objectId, Base overrideParent = null) where TU : Object
        {
            var objectResult = (CheckInactiveObjects(objectId) ?? SpawnNewObject(objectId, overrideParent));
            _activeObjects.Add(objectResult as T);
            ActiveObjects.SetValue(ActiveObjects.GetValue() + 1);
            objectResult.OnObjectDeactivated += Object_OnDeath;
            return objectResult as TU;
        }

        private int _objectCount;
        protected int ObjectCount { get { return _objectCount++; } }
        protected virtual T SpawnNewObject(string id, Base overrideParent = null)
        {
            var modelToCopy = Root.GetObjectModel(id);
            var objectModel = Copier.CopyAs<ObjectModel>(modelToCopy);
            objectModel.Id = string.Format("{0}_{1}", objectModel.Id, Guid.NewGuid());
            objectModel.Type = id;
            var newObject = Root.IoCContainer.GetInstance<Object>(objectModel.GetType(), new System.Object[] {objectModel, overrideParent ?? this});

            if (newObject == null)
                throw new EngineException(this, string.Format("Failed to instantiate {0}:{1} as {2}", objectModel.GetType(), id, typeof(Object)));
            
            return newObject as T;
        }

        public readonly AdjustableProperty<int> ActiveObjects;
        private readonly List<T> _activeObjects = new List<T>();

        private void Object_OnDeath(Object objectViewModel)
        {
            var objectT = (T) objectViewModel;
            _activeObjects.Remove(objectT);
            ActiveObjects.SetValue(ActiveObjects.GetValue() - 1);
            AddToInactiveObjectList(objectT);
        }
        private void AddToInactiveObjectList(T inactiveObject)
        {
            var objectType = inactiveObject.Type;

            if (InactiveObjects.ContainsKey(objectType))
                InactiveObjects[objectType].Add(inactiveObject);
            else
                InactiveObjects.Add(objectType, new List<Object> { inactiveObject });
        }

        private static readonly Dictionary<string, List<Object>> InactiveObjects = new Dictionary<string, List<Object>>();
        private static bool _isDestructionInProgress;
        private static void DestroyInactiveObjects()
        {
            if (_isDestructionInProgress) return;

            _isDestructionInProgress = true;
            foreach (var inactiveObjects in InactiveObjects.Values)
                foreach (var inactiveObject in inactiveObjects)
                    inactiveObject.Destroy();

            InactiveObjects.Clear();
            _isDestructionInProgress = false;
        }

        private Object CheckInactiveObjects(string objectId)
        {
            // Id is not registered yet
            if (!InactiveObjects.ContainsKey(objectId)) return null;

            var objectList = InactiveObjects[objectId];

            // Id is registered, but we don't have any copies of that inactiveObject
            if (objectList.Count == 0) return null;

            // We have some, now give them one, just one
            var foundObject = objectList[0];
            objectList.Remove(foundObject);

            return foundObject;
        }
        #endregion

        protected override void OnDestroyed()
        {
            foreach (var activeObject in _activeObjects)
                activeObject.OnObjectDeactivated -= Object_OnDeath;

            foreach (var activeObject in _activeObjects)
                activeObject.Destroy();

            _activeObjects.Clear();

            DestroyInactiveObjects();

            base.OnDestroyed();
        }
    }

    public abstract class IntervalBase : Element
    {
        protected IntervalBase(IntervalModel model, Base parent) : base(model, parent) { }
        public readonly Property<float> Interval = new Property<float>();
    }
}
