using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class ShootingGUI : BaseController
    {
        private ShooterViewModel _shooterView;
        protected override void OnSetup()
        {
            base.OnSetup();

            _shooterView = View as ShooterViewModel;
            
            _clickArea = new Rect(
                (Screen.width * _shooterView.Index) - (_shooterView.Index * Screen.height * Values.GUI_CIRCLE_SIZE_F),
                Screen.height * (1 - Values.GUI_CIRCLE_SIZE_F),
                Screen.height * Values.GUI_CIRCLE_SIZE_F,
                Screen.height * Values.GUI_CIRCLE_SIZE_F);
            _clickCheckArea = _clickArea;
            _clickCheckArea.y = 0f;

            _image = Resources.Load<Texture2D>("GUIs/Circle");
        }

        private Texture2D _image;

        private void OnGUI()
        {
            GUI.DrawTexture(_clickArea, _image);
        }

        private Rect _clickArea;
        private Rect _clickCheckArea;

        private int _lastTouchIndex;
        private void Update()
        {
            if (Input.touches.Length <= _lastTouchIndex)
                _shooterView.StopShooting();
            else if (Input.touches.Length >= _lastTouchIndex && !_clickCheckArea.Contains(Input.touches[_lastTouchIndex].position))
                _shooterView.StopShooting();

            for (var i = 0; i < Input.touches.Length; i++)
            {
                var touch = Input.touches[i];
                if (_clickCheckArea.Contains(touch.position))
                {
                    _lastTouchIndex = i;
                    _shooterView.StartShooting();
                }
            }
        }
    }
}
