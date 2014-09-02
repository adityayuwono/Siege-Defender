using System.Collections.Generic;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class PlayerViewModel : ElementViewModel
    {
        private readonly PlayerModel _model;

        public PlayerViewModel(PlayerModel model, SceneViewModel parent) : base(model, parent)
        {
            _model = model;

            foreach (var shooterModel in _model.Shooters)
            {
                var shooterVM = new ShooterViewModel(shooterModel, this);
                Shooters.Add(shooterVM);
                Elements.Add(shooterVM);
            }
        }

        public readonly List<ShooterViewModel> Shooters = new List<ShooterViewModel>();
    }
}
