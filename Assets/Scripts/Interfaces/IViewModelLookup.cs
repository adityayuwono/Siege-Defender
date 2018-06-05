using Scripts.ViewModels;

namespace Scripts.Interfaces
{
	public interface IViewModelLookup
	{
		void RegisterToLookup(Base viewModel);

		void UnregisterFromLookup(Base viewModel);

		T GetViewModelAsType<T>(string id) where T : Base;
	}
}
