using Scripts.Helpers;
using UnityEngine;

namespace Scripts.Components
{
    public class AimingController : BaseTexturedController
    {
        private const float TOUCH_DEVIATION = 1.2f;
        private const string CROSSHAIR_ASSET_PATH = "GUIs/Crosshair";

        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = GameObject.Find("Player").camera;
        }

        private Texture2D _crosshairImage;
        private static Vector2 _halfScreen;

        protected override void OnSetup()
        {
            base.OnSetup();
            
            _halfScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);

            _crosshairImage = Resources.Load<Texture2D>(CROSSHAIR_ASSET_PATH);
            _crosshairRect = new Rect(Screen.height / 2f, Screen.width / 2f, Screen.height * Values.GUI_CROSSHAIR_SIZE_F, Screen.height * Values.GUI_CROSSHAIR_SIZE_F);
        }

        protected override void OnChange()
        {
            base.OnChange();

            _circleCenter = TextureScreenArea.center;
        }

        private Vector2 _circleCenter;
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
            else
            {
                ProcessTouchOrMouse(Input.mousePosition);
            }
        }

        private void ProcessTouchOrMouse(Vector2 inputPosition)
        {
            inputPosition.y = Screen.height - inputPosition.y;
            if (TextureScreenArea.Contains(inputPosition))
            {
                var relativeToCenter = (inputPosition - _circleCenter) * TOUCH_DEVIATION;

                _crosshairRect.x = _halfScreen.x + (_halfScreen.x * (relativeToCenter.x / (TextureScreenArea.width / 2f)));
                _crosshairRect.y = _halfScreen.y + (_halfScreen.y * (relativeToCenter.y / (TextureScreenArea.height / 2f)));

                UpdateObjectPosition(new Vector3(
                _crosshairRect.x + (Screen.height * Values.GUI_CROSSHAIR_HALFSIZE_F),
                (Screen.height - _crosshairRect.y) - (Screen.height * Values.GUI_CROSSHAIR_HALFSIZE_F), 0));
            }
        }

        private void UpdateObjectPosition(Vector3 inputPosition)
        {
            // Bug after destruction the MainCamera reference is not cleared, or maybe we fail to hook the new camera at Start
            if (_mainCamera == null)
                _mainCamera = GameObject.Find("Player").camera;

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
