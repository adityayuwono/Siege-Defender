﻿// ReSharper disable RedundantUsingDirective
using System.Linq;// This is used when iterating inputs, but it's only in Android
// ReSharper restore RedundantUsingDirective
using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    /// <summary>
    /// Drag to aim, stay to shoot repeatedly
    /// </summary>
    public class ShootingGUI : BaseController
    {
        private Shooter _shooterView;
        protected override void OnSetup()
        {
            base.OnSetup();

            _shooterView = ViewModel as Shooter;
            
            _clickArea = new Rect(
                (Screen.width * _shooterView.Index) - (_shooterView.Index * Screen.height * Values.GUI_CIRCLE_SIZE_F),
                Screen.height * (1 - Values.GUI_CIRCLE_SIZE_F),
                Screen.height * Values.GUI_CIRCLE_SIZE_F,
                Screen.height * Values.GUI_CIRCLE_SIZE_F);
            _clickCheckArea = _clickArea;
            _clickCheckArea.y = 0f;
        }

        private Rect _clickArea;
        private Rect _clickCheckArea;

        private void Update()
        {
#if !UNITY_EDITOR
                // If Android
                if (Input.touches.Any(touch => _clickCheckArea.Contains(touch.position)))
                    _shooterView.StartShooting();
                else
                    _shooterView.StopShooting();
#else
                // If Mouse, for testing purposes only
                if (Input.GetMouseButton(0) && _clickCheckArea.Contains(Input.mousePosition))
                    _shooterView.StartShooting();
                else
                    _shooterView.StopShooting();
#endif
        }
    }
}
