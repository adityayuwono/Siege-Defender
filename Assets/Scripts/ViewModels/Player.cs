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

            var index = 0;
            foreach (var shooterModel in _model.Shooters)
            {
                var shooterVM = new Shooter(index, shooterModel, this);
                _shooters.Add(shooterVM);
                Elements.Add(shooterVM);
                index++;
            }
        }

        private readonly List<Shooter> _shooters = new List<Shooter>();
    }
}
