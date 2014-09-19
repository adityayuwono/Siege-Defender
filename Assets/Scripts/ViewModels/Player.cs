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
                Shooters.Add(shooterVM);
                Elements.Add(shooterVM);
            }
        }

        public readonly List<Shooter> Shooters = new List<Shooter>();
    }
}
