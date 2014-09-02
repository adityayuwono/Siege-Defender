using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class FollowMouse : MouseInteraction
    {
        private const float TOUCH_DEVIATION = 1.2f;
        private const string CROSSHAIR_ASSET_PATH = "GUIs/Crosshair";

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = GameObject.Find("Player").camera;
        }

        private Texture2D _crosshairImage;
        private TargetViewModel _viewModel;
        private static Vector2 _halfScreen;

        protected override void OnSetup()
        {
            base.OnSetup();
            _viewModel = ViewModel as TargetViewModel;
            
            _halfScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);

            _crosshairImage = Resources.Load<Texture2D>(CROSSHAIR_ASSET_PATH);
            
            _circleRect = new Rect(
                (Screen.width * _viewModel.Index) - (_viewModel.Index * Screen.height * Values.GUI_CIRCLE_SIZE_F),
                Screen.height * (1 - Values.GUI_CIRCLE_SIZE_F),
                Screen.height * Values.GUI_CIRCLE_SIZE_F,
                Screen.height * Values.GUI_CIRCLE_SIZE_F);

            _circleCenter = _circleRect.center;

            _crosshairRect = new Rect(Screen.height / 2f, Screen.width / 2f, Screen.height * Values.GUI_CROSSHAIR_SIZE_F, Screen.height * Values.GUI_CROSSHAIR_SIZE_F);
        }


        private Rect _circleRect;
        private Vector2 _circleCenter;
        private Rect _crosshairRect;
        private void OnGUI()
        {
            GUI.DrawTexture(_crosshairRect, _crosshairImage);
        }


        protected override void ProcessTouchOrMouse(Vector2 inputPosition)
        {
            inputPosition.y = Screen.height - inputPosition.y;
            if (_circleRect.Contains(inputPosition))
            {
                var relativeToCenter = (inputPosition - _circleCenter) * TOUCH_DEVIATION;

                _crosshairRect.x = _halfScreen.x + (_halfScreen.x * (relativeToCenter.x / (_circleRect.width / 2f)));
                _crosshairRect.y = _halfScreen.y + (_halfScreen.y * (relativeToCenter.y / (_circleRect.height/ 2f)));

                UpdateObjectPosition(new Vector3(
                _crosshairRect.x + (Screen.height * Values.GUI_CROSSHAIR_HALFSIZE_F),
                (Screen.height - _crosshairRect.y) - (Screen.height * Values.GUI_CROSSHAIR_HALFSIZE_F), 0));
            }
        }

        private void UpdateObjectPosition(Vector3 inputPosition)
        {
            var ray = _mainCamera.ScreenPointToRay(inputPosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, Values.CROSSHAIR_LAYERMASK))
            {
                var position = _mainCamera.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, hitInfo.distance));
                transform.position = position;
            }
        }
    }
}
