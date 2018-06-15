using System;
using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
	public class BloodOverlay : Percentage
	{
		public Action Damaged;

		public BloodOverlay(BloodOverlayModel model, Base parent) : base(model, parent)
		{
		}
	}
}
