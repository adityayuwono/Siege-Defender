using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Contexts;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.ViewModels;
using Scripts.Views;
using UnityEngine.SceneManagement;

namespace Scripts
{
	public abstract class RootBase : Base, IRoot
	{
		private readonly BaseContext _context;

		protected RootBase(RootModel model, BaseContext parent)
			: base(model, null)
		{
			Randomizer = new Random();
			_context = parent;

			PropertyLookup = new PropertyLookup(this, this); // This is the root
			ResourceManager = new ResourcePooler(this);
		}

		public Random Randomizer { get; private set; }

		public BaseContext Context
		{
			get { return _context; }
		}

		public abstract IIntervalRunner IntervalRunner { get; }
		
		public IResource ResourceManager { get; private set; }

		public PropertyLookup PropertyLookup { get; private set; }
		
		public abstract void StartCoroutine(IEnumerator coroutine);

		public void ChangeScene(string sceneName, string levelId)
		{
			Hide("Change Scene");
			Deactivate("Change Scene");
			DataContext.Instance.LevelId = levelId;
			SceneManager.LoadScene(sceneName);
		}

		#region View Model Lookup
		private readonly Dictionary<string, Base> _vmLookup = new Dictionary<string, Base>();

		public void RegisterToLookup(Base viewModel)
		{
			if (_vmLookup.ContainsKey(viewModel.Id))
			{
				return;
			}

			_vmLookup.Add(viewModel.Id, viewModel);
		}

		public void UnregisterFromLookup(Base viewModel)
		{
			if (!_vmLookup.ContainsKey(viewModel.Id))
			{
				return;
			}

			_vmLookup.Remove(viewModel.Id);
		}

		public T GetViewModelAsType<T>(string id) where T : Base
		{
			if (!_vmLookup.ContainsKey(id))
			{
				throw new EngineException(this, string.Format("ViewModel {0} is not registered", id));
			}

			var foundViewModel = _vmLookup[id];
			if (foundViewModel == null)
			{
				throw new EngineException(this, string.Format("ViewModel {0} is not convertable to {1}", id, typeof(T)));
			}

			var foundViewModelAsT = foundViewModel as T;

			return foundViewModelAsT;
		}
		#endregion

		#region View Lookup Pool
		private readonly Dictionary<string, BaseView> _views = new Dictionary<string, BaseView>();

		public void RegisterView(Base viewModel, BaseView view)
		{
			if (_views.ContainsKey(viewModel.FullId))
			{
				throw new EngineException(this,
					string.Format("Failed to register View of Type: {1}, duplicate for Id: {0}", viewModel.Id, viewModel.GetType()));
			}

			_views.Add(viewModel.FullId, view);
		}

		public void UnregisterView(Base viewModel)
		{
			_views.Remove(viewModel.FullId);
		}

		public T GetView<T>(Base viewModel) where T : BaseView
		{
			var id = viewModel.FullId;
			if (!_views.ContainsKey(id))
			{
				throw new EngineException(this, string.Format("Failed to get view for Id: {0}", id));
			}

			return _views[id] as T;
		}
		#endregion
	}
}