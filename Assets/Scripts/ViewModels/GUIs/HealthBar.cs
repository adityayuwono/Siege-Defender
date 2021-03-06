using Scripts.Models.GUIs;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels.GUIs
{
	public class HealthBar : Object
	{
		public HealthBar(HealthBarModel model, ViewModels.Base parent) : base(model, parent)
		{
		}

		public LivingObject LivingObject { get; private set; }

		public void Activate(LivingObject livingObject)
		{
			LivingObject = livingObject;
			Activate();
		}
	}
}
