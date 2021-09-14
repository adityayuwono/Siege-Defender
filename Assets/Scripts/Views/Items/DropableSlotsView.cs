using Scripts.Components;
using Scripts.ViewModels.Items;

namespace Scripts.Views.Items
{
    public class DropableSlotsView : ElementView
    {
		private readonly DropableSlots _viewModel;

		public DropableSlotsView(DropableSlots viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			GameObject.AddComponent<DragDropContainerController>().OnDropped += _viewModel.HandleObjectDropped;
		}

		protected override void OnDestroy()
		{
			GameObject.GetComponent<DragDropContainerController>().OnDropped -= _viewModel.HandleObjectDropped;

			base.OnDestroy();
		}
	}
}
