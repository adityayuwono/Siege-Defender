using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class SpecialEffectManager : Interval<SpecialEffect>
    {
        private readonly SpecialEffectManagerModel _model;
        public SpecialEffectManager(SpecialEffectManagerModel model, Object parent) : base(model, parent)
        {
            _model = model;
        }
		
        public void DisplaySpecialEffect(string id, Vector3 position)
        {
            var specialEffect = GetObject<SpecialEffect>(id);
            specialEffect.ShowSpecialEffect(position);
        }

        public void DisplaySpecialEffect(string id, Object parent)
        {
            var specialEffect = GetObject<SpecialEffect>(id);
            specialEffect.ShowSpecialEffect(parent);
        }
	    protected override void OnLoad()
	    {
		    base.OnLoad();

		    Root.SpecialEffectManager = this;
	    }
    }
}
