using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class FollowMouse : MouseInteraction
    {
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = GameObject.Find("Player").camera;
        }

        private Texture2D _crosshairImage;

        private TargetViewModel _viewModel;
        protected override void OnSetup()
        {
            base.OnSetup();

            _viewModel = ViewModel as TargetViewModel;

            _crosshairImage = Resources.Load<Texture2D>("GUIs/Crosshair");

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
                var relativeToCenter = (inputPosition - _circleCenter)*1.2f;

                _crosshairRect.x = (Screen.width / 2f) + ((Screen.width / 2f) * (relativeToCenter.x / (_circleRect.width / 2f)));
                _crosshairRect.y = (Screen.height / 2f) + ((Screen.height / 2f) * (relativeToCenter.y / (_circleRect.height/ 2f)));

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
