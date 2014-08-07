using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class FollowMouse : BaseController
    {
        public Camera MainCamera;

        private void Start()
        {
            MainCamera = GameObject.Find("Player").camera;
        }

        private Texture2D _image;
        private Texture2D _crosshairImage;

        private TargetViewModel _viewModel;
        protected override void OnSetup()
        {
            base.OnSetup();

            _viewModel = View as TargetViewModel;

            _crosshairImage = Resources.Load<Texture2D>("GUIs/Crosshair");

            _circleRect = new Rect(
                (Screen.width * _viewModel.Index) - (_viewModel.Index * Screen.height * Values.GUI_CIRCLE_SIZE_F),
                Screen.height * (1 - Values.GUI_CIRCLE_SIZE_F),
                Screen.height * Values.GUI_CIRCLE_SIZE_F,
                Screen.height * Values.GUI_CIRCLE_SIZE_F);
            _crosshairRect = new Rect(Screen.height / 2f, Screen.width / 2f, Screen.height * Values.GUI_CROSSHAIR_SIZE_F, Screen.height * Values.GUI_CROSSHAIR_SIZE_F);
        }


        private Rect _circleRect;
        private Rect _crosshairRect;
        private void OnGUI()
        {
            GUI.DrawTexture(_crosshairRect, _crosshairImage);
        }


        private void Update()
        {
            if (Input.touches.Length > 0)
            {
                foreach (var touch in Input.touches)
                {
                    ProcessTouchOrMouse(touch.position);
                }
            }
        }

        private void ProcessTouchOrMouse(Vector2 inputPosition)
        {
            inputPosition.y = Screen.height - inputPosition.y;
            if (_circleRect.Contains(inputPosition))
            {
                var relativeX = inputPosition.x - _circleRect.x;
                _crosshairRect.x = Screen.width / (Screen.height * Values.GUI_CIRCLE_SIZE_F / relativeX);
                var relativeY = inputPosition.y - _circleRect.y;
                _crosshairRect.y = Screen.height / (Screen.height * Values.GUI_CIRCLE_SIZE_F / relativeY);

                UpdateObjectPosition(new Vector3(
                _crosshairRect.x + (Screen.height * Values.GUI_CROSSHAIR_HALFSIZE_F),
                (Screen.height - _crosshairRect.y) - (Screen.height * Values.GUI_CROSSHAIR_HALFSIZE_F), 0));
            }
        }

        private void UpdateObjectPosition(Vector3 inputPosition)
        {
            var ray = MainCamera.ScreenPointToRay(inputPosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, ~(0 << 8)))
            {
                var position = MainCamera.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, hitInfo.distance));
                transform.position = position;
            }
        }
    }
}
