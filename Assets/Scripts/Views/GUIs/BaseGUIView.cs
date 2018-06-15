using Scripts.ViewModels.GUIs;
using UnityEngine.UI;

namespace Scripts.Views.GUIs
{
	public class BaseGUIView : ElementView
	{
		public BaseGUIView(BaseGUI viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
		}

		protected Image Image { get; private set; }

		protected override void OnLoad()
		{
			base.OnLoad();

			Image = GameObject.GetComponent<Image>();
		}
	}
}