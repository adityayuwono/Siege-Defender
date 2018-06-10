using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
	public class PlayerView : ElementView
	{
		public PlayerView(Player viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
		}

		protected override void OnShow()
		{
			base.OnShow();

			AttachController<AccelerometerController>();
		}
	}
}