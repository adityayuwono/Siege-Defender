using System.Collections.Generic;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class Player : Element
    {
        private readonly PlayerModel _model;

        public Player(PlayerModel model, Scene parent) : base(model, parent)
        {
            _model = model;

            foreach (var shooterModel in _model.Shooters)
            {
                var shooterVM = new Shooter(shooterModel, this);
                _shooters.Add(shooterVM);
                Elements.Add(shooterVM);
            }
        }

        private readonly List<Shooter> _shooters = new List<Shooter>();
    }
}
