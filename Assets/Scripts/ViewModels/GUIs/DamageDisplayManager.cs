using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
    public class DamageDisplayManager : IntervalViewModel<DamageGUI>
    {
        private DamageDisplay_GUIModel _model;
        public DamageDisplayManager(DamageDisplay_GUIModel model, ObjectViewModel parent) : base(model, parent)
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
