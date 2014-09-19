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

        private Vector3 _position;
        public override Vector3 Position
        {
            get { return _position; }
        }

        public float HideDelay
        {
            get { return 1f; }
        }

        public void ShowDamage(float damage, Vector3 position)
        {
            Text.SetValue(Math.Round(damage).ToString(CultureInfo.InvariantCulture));
            _position = position;

            Activate();
            Show();
            Hide("Only Display for a short time");
        }
    }
}
