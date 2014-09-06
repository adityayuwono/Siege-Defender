using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
    public class DamageDisplayManager : IntervalViewModel<DamageGUI>
    {
        private DamageDisplayModel _model;
        public DamageDisplayManager(DamageDisplayModel model, ObjectViewModel parent) : base(model, parent)
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
