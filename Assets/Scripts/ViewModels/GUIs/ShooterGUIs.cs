using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
	public class ShooterGUI : BaseGUI
	{
		private readonly ShooterModel _model;

		public ShooterGUI(ShooterModel model, Object parent) 
			: base(model, parent)
		{
			_model = model;
		}

		public Shooter Shooter { get; private set; }

		public string AimingAssetId
		{
			get { return _model.AimingAssetId; }
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			Shooter = Root.GetViewModelAsType<Shooter>(_model.ShooterTarget);
		}

		protected override void OnDestroyed()
		{
			Shooter = null;

			base.OnDestroyed();
		}
	}
}