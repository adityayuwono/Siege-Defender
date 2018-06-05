using Scripts.Helpers;
using Scripts.Interfaces;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Components
{
	public class BaseController : MonoBehaviour, IBase
	{
		private bool _isSet;
		protected Object ViewModel { get; private set; }

		public string Id
		{
			get { return ViewModel.Id; }
		}

		public string FullId
		{
			get { return ViewModel.Id; }
		}

		public void Setup(Object viewModel)
		{
			_isSet = true;
			ViewModel = viewModel;

			OnSetup();
		}

		protected virtual void OnSetup()
		{
		}

		private void Start()
		{
			if (!_isSet) throw new EngineException(this, "Failed to continue, need Setup first");
		}
	}
}