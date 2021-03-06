using Scripts.Extensions;
using Scripts.Helpers;
using UnityEngine;

namespace Scripts.Components
{
	public class AimingController : BaseTexturedController
	{
		private const float TouchDeviation = 1.2f;
		private const string CrosshairAssetPath = "GUIs/Crosshair";
		private static Vector2 _halfScreen;

		private Texture2D _crosshairImage;
		private Rect _crosshairRect;

		private Camera _mainCamera;

		private void Start()
		{
			_mainCamera = GameObject.Find("Player").GetComponent<Camera>();
		}

		protected override void OnSetup()
		{
			base.OnSetup();

			_halfScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);

			_crosshairImage = Resources.Load<Texture2D>(CrosshairAssetPath);
			_crosshairRect = new Rect(
				Screen.height / 2f,
				Screen.width / 2f,
				Screen.height * Values.GuiCrosshairSizeF,
				Screen.height * Values.GuiCrosshairSizeF);
		}

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
			var rectTransform = MainTexture.GetComponent<RectTransform>();
			var isRectangleContainsMouse = RectTransformUtility.RectangleContainsScreenPoint(rectTransform, inputPosition, Camera.main);

			if (isRectangleContainsMouse)
			{
				var screenRect = rectTransform.ToScreenSpace();
				var normalizedInputPosition = new Vector2(inputPosition.x, Screen.height - inputPosition.y);
				var relativeToCenter = (normalizedInputPosition - screenRect.center) * TouchDeviation;

				_crosshairRect.x = _halfScreen.x + _halfScreen.x * (relativeToCenter.x / (screenRect.width/2f));
				_crosshairRect.y = _halfScreen.y + _halfScreen.y * (relativeToCenter.y / (screenRect.height/2f));

				UpdateObjectPosition(new Vector3(
					_crosshairRect.x + Screen.height * Values.GuiCrosshairHalfsizeF,
					Screen.height - _crosshairRect.y - Screen.height * Values.GuiCrosshairHalfsizeF, 0));
			}
		}

		private void UpdateObjectPosition(Vector3 inputPosition)
		{
			_mainCamera = GameObject.Find("Player").GetComponent<Camera>();

			var ray = _mainCamera.ScreenPointToRay(inputPosition);
			RaycastHit hitInfo;

			if (Physics.Raycast(ray, out hitInfo, float.PositiveInfinity, Values.CrosshairLayermask))
			{
				var position = _mainCamera.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, hitInfo.distance));
				transform.position = position;
			}
		}
	}
}