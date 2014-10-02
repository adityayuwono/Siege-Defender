using System.Collections.Generic;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class Interval<T> : IntervalBase where T : Object
    {
        private readonly IntervalModel _model;

        protected Interval(IntervalModel model, Object parent) : base(model, parent)
        {
            _model = model;

            ActiveObjects = new AdjustableProperty<int>("ActiveObjects", this);
            ActiveObjects.SetValue(0);

            Interval.SetValue(_model.Interval);
        }

        public override void Hide(string reason)
        {
            foreach (var activeObject in _activeObjects)
                activeObject.Hide(reason);

            base.Hide(reason);
        }

        #region Spawning Objects

        protected TU GetObject<TU>(string objectId) where TU : T
        {
            var objectResult = (CheckInactiveObjects(objectId) ?? SpawnNewObject(objectId));
            _activeObjects.Add(objectResult);
            ActiveObjects.SetValue(ActiveObjects.GetValue() + 1);
            objectResult.OnObjectDeactivated += Object_OnDeath;
            return objectResult as TU;
        }

        private int _objectCount;
        protected int ObjectCount { get { return _objectCount++; } }
        protected virtual T SpawnNewObject(string id)
        {
            var modelToCopy = Root.GetObjectModel(id);
            var objectModel = Copier.CopyAs<ObjectModel>(modelToCopy);
            objectModel.Id = string.Format("{0}_{1}_{2}", objectModel.Id, Id, ObjectCount);
            objectModel.Type = id;
            var newObject = Root.IoCContainer.GetInstance<T>(objectModel.GetType(), new System.Object[] {objectModel, this});

            if (newObject == null)
                throw new EngineException(this, string.Format("Failed to instantiate {0}:{1} as {2}", objectModel.GetType(), id, typeof(T)));
            
            return newObject;
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

            if (_inactiveObjects.ContainsKey(objectType))
                _inactiveObjects[objectType].Add(inactiveObject);
            else
                _inactiveObjects.Add(objectType, new List<T> { inactiveObject });
        }
        
        private readonly Dictionary<string, List<T>> _inactiveObjects = new Dictionary<string, List<T>>();
        private T CheckInactiveObjects(string objectId)
        {
            // Id is not registered yet
            if (!_inactiveObjects.ContainsKey(objectId)) return null;

            var objectList = _inactiveObjects[objectId];

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

            foreach (var inactiveObjectKVP in _inactiveObjects)
                foreach (var inactiveObject in inactiveObjectKVP.Value)
                    inactiveObject.Destroy();

            _activeObjects.Clear();
            _inactiveObjects.Clear();

            base.OnDestroyed();
        }
    }

    public abstract class IntervalBase : Element
    {
        protected IntervalBase(IntervalModel model, Object parent) : base(model, parent) { }
        public readonly Property<float> Interval = new Property<float>();
    }
}
