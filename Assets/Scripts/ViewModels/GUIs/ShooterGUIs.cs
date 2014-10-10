using System.Collections.Generic;
using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
    public class ShooterGUIs : BaseGUI
    {
        private readonly ShooterGUIsModel _model;

        public ShooterGUIs(ShooterGUIsModel model, Object parent) : base(model, parent)
        {
            _model = model;

            foreach (var shooterGUIModel in _model.ShooterGUIs)
                _shooterGUIs.Add(new ShooterGUI(shooterGUIModel, this));
        }

        private readonly List<ShooterGUI> _shooterGUIs = new List<ShooterGUI>();

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var shooterGUI in _shooterGUIs)
                shooterGUI.Activate();
        }

        public override void Show()
        {
            base.Show();

            foreach (var shooterGUI in _shooterGUIs)
                shooterGUI.Show();
        }

        public override void Hide(string reason)
        {
            foreach (var shooterGUI in _shooterGUIs)
                shooterGUI.Hide("Hiding Shooter GUIs");

            base.Hide(reason);
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();

            foreach (var shooterGUI in _shooterGUIs)
                shooterGUI.Destroy();
        }
    }

    public class ShooterGUI : BaseGUI
    {
        private readonly ShooterGUIModel _model;

        public ShooterGUI(ShooterGUIModel model, ShooterGUIs parent) : base(model, parent)
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
