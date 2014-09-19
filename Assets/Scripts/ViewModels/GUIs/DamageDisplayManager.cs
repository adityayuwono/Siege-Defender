using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
    public class DamageDisplayManager : Interval<DamageGUI>
    {
        private DamageDisplayGUIModel _model;
        public DamageDisplayManager(DamageDisplayGUIModel model, Object parent) : base(model, parent)
        {
            _model = model;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            Root.DamageDisplay = this;
        }

        public void DisplayDamage(float damage, Vector3 position)
        {
            var damageGUI = GetObject<DamageGUI>(_model.DamageGUI);
            damageGUI.ShowDamage(damage, position);
        }
    }
}
