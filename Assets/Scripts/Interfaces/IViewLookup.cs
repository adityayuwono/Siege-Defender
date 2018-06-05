using Scripts.ViewModels;
using Scripts.Views;

namespace Scripts.Interfaces
{
	public interface IViewLookup
	{
		void RegisterView(Base viewModel, BaseView view);

		void UnregisterView(Base viewModel);

		T GetView<T>(Base viewModel) where T : BaseView;
	}
}
