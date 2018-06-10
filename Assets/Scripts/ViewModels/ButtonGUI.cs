using System;
using Scripts.Models.GUIs;
using Scripts.ViewModels.GUIs;

namespace Scripts.ViewModels
{
	public class ButtonGUI : BaseGUI
	{
		public event Action OnClick;

		public ButtonGUI(ButtonGUIModel model, Base parent) : base(model, parent)
		{
		}

		public void OnClicked()
		{
			// Invoke all actions related to this button
			if (OnClick != null)
			{
				OnClick();
			}
		}
	}
}