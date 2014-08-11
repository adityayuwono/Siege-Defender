using System.Linq;
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

            _shooterView = ViewModel as ShooterViewModel;
            
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

        private void Update()
        {
            if (Input.touches.Length > 0)
            {
                // If Android
                if (Input.touches.Any(touch => _clickCheckArea.Contains(touch.position)))
                    _shooterView.StartShooting();
                else
                    _shooterView.StopShooting();
            }
            else
            {
                // If Mouse, for testing purposes only
                if (Input.GetMouseButton(0))
                    _shooterView.StartShooting();
                else
                    _shooterView.StopShooting();
            }
        }
    }
}
