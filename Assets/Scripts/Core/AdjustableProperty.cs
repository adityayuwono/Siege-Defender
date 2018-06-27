using System;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.ViewModels;

namespace Scripts.Core
{
	public class AdjustableProperty<T> : Property<T>, IDisposable
	{
		private readonly IContext _context;
		private readonly Base _viewModel;

		public AdjustableProperty(string propertyId, Base baseViewModel, bool isAlwaysChanging = false) : base(
			propertyId, isAlwaysChanging)
		{
			_viewModel = baseViewModel;
			_context = baseViewModel as IContext ?? baseViewModel.GetParent<IContext>();

			if (_context == null)
			{
				throw new EngineException(baseViewModel, "Failed to find Context in the hierarchy");
			}

			_context.PropertyLookup.RegisterProperty(baseViewModel, PropertyId, this);

			baseViewModel.Properties.Add(this);
		}

		public void Dispose()
		{
			_context.PropertyLookup.UnregisterProperty(_viewModel, PropertyId);
		}
	}
}