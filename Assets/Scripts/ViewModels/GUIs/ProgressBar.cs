using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
	public class ProgressBar : Percentage
	{
		public ProgressBar(ProgressBarModel model, Object parent) 
			: base(model, parent)
		{
		}
	}
}