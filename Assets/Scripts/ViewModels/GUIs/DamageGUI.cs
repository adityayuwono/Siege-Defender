using System;
using System.Globalization;
using Scripts.Models.GUIs;
using UnityEngine;

namespace Scripts.ViewModels.GUIs
{
    public class DamageGUI : LabelGUI
    {
        private DamageGUIModel _model;
        public DamageGUI(DamageGUIModel model, Base parent) : base(model, parent)
        {
            _model = model;
        }

        public float HideDelay
        {
            get { return 0.5f; }
        }

        public void ShowDamage(float damage, Vector3 position)
        {
            Text.SetValue(Math.Round(damage).ToString(CultureInfo.InvariantCulture));
            Position = position;

            Activate();
            Show();
            Hide("Only Display for a short time");
        }
    }
}
