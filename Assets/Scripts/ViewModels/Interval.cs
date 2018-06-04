using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models;

namespace Scripts.ViewModels
{
	public class Interval<T> : IntervalBase where T : Object
	{
		public readonly AdjustableProperty<int> ActiveObjects;

		private readonly IntervalModel _model;
		private readonly List<T> _activeObjects = new List<T>();

		protected Interval(IntervalModel model, Base parent)
			: base(model, parent)
		{
			_model = model;

			ActiveObjects = new AdjustableProperty<int>("ActiveObjects", this);
			ActiveObjects.SetValue(0);

			Interval.SetValue(_model.Interval);
		}

		protected int ObjectCount { get { return _objectCount++; } }
		private int _objectCount;

		public override void Show()
		{
			base.Show();

			// Reset the active objects count
			ActiveObjects.SetValue(0);
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

		protected override void OnDestroyed()
		{
			foreach (var activeObject in _activeObjects)
			{
				activeObject.OnObjectDeactivated -= Object_OnDeath;
			}

			foreach (var activeObject in _activeObjects)
			{
				activeObject.Destroy();
			}

			_activeObjects.Clear();

			DestroyInactiveObjects();

			base.OnDestroyed();
		}

		#region Spawning Objects
		protected TU GetObject<TU>(string objectId, Base overrideParent = null) where TU : ViewModels.Object
		{
			var objectResult = (CheckInactiveObjects(objectId) ?? SpawnNewObject(objectId, overrideParent));
			if (objectResult as T == null)
			{
				throw new EngineException(this, string.Format("Failed to cast '{0}'\ntype of ({1}) to ({2})", objectResult.Id, objectResult.GetType(), typeof(T)));
			}

			_activeObjects.Add(objectResult as T);
			ActiveObjects.SetValue(ActiveObjects.GetValue() + 1);
			objectResult.OnObjectDeactivated += Object_OnDeath;
			return objectResult as TU;
		}

		protected virtual T SpawnNewObject(string id, Base overrideParent = null)
		{
			var modelToCopy = DataContext.GetObjectModel(this, id);
			var objectModel = CreateNewModel(id, modelToCopy);
			var newObject = IoC.IoCContainer.GetInstance<Object>(objectModel.GetType(), new object[] { objectModel, overrideParent ?? this });
			if (newObject == null)
			{
				throw new EngineException(this, string.Format("Failed to instantiate {0}:{1} as {2}", objectModel.GetType(), id, typeof(global::Scripts.ViewModels.Object)));
			}

			return newObject as T;
		}

		private ObjectModel CreateNewModel(string id, ObjectModel modelToCopy)
		{
			var objectModel = Copier.CopyAs<ObjectModel>(modelToCopy);
			objectModel.Id = string.Format("{0}_{1}", objectModel.Id, Guid.NewGuid());
			objectModel.Type = id;

			var actions = objectModel.Triggers.SelectMany(t => t.Actions).ToList();
			var conditions = objectModel.Triggers.SelectMany(t => t.Conditions).ToList();
			foreach (var childElement in objectModel.Elements)
			{
				if (!string.IsNullOrEmpty(childElement.Id) && childElement.Id.Contains("[x]"))
				{
					var originalId = childElement.Id;
					childElement.Id = string.Format("{0}_{1}", childElement.Id, Guid.NewGuid()); ;

					foreach (var action in actions)
					{
						action.Target = action.Target.Replace(originalId, childElement.Id);
					}
					foreach (var action in conditions)
					{
						action.Target = action.Target.Replace(originalId, childElement.Id);
					}
				}
			}

			return objectModel;
		}

		private void Object_OnDeath(Object objectViewModel)
		{
			var objectT = (T)objectViewModel;
			_activeObjects.Remove(objectT);
			ActiveObjects.SetValue(ActiveObjects.GetValue() - 1);
			AddToInactiveObjectList(objectT);
		}

		private void AddToInactiveObjectList(T inactiveObject)
		{
			var objectType = inactiveObject.Type;

			if (InactiveObjects.ContainsKey(objectType))
			{
				InactiveObjects[objectType].Add(inactiveObject);
			}
			else
			{
				InactiveObjects.Add(objectType, new List<Object> { inactiveObject });
			}
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
	}
}
