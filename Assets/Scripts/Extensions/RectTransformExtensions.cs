using UnityEngine;

namespace Scripts.Extensions
{
	public static class RectTransformExtensions
	{
		public static Rect ToScreenSpace(this RectTransform transform)
		{
			var canvas = transform.GetComponentInParent<Canvas>().GetComponent<RectTransform>().sizeDelta;
			var sizeFactor = new Vector2(transform.sizeDelta.x / canvas.x, transform.sizeDelta.y / canvas.y);
			var size = new Vector2(Screen.width * sizeFactor.x, Screen.height * sizeFactor.y);

			var x = transform.position.x + transform.anchoredPosition.x + (transform.position.x > 0 ? Screen.width - size.x : 0);
			var y = Screen.height - transform.position.y - transform.anchoredPosition.y - size.y;

			return new Rect(x, y, size.x, size.y);
		}
	}
}
