using UnityEngine;

namespace Scripts.Components.UI
{
	public class Table : MonoBehaviour
	{
		public int Column = 8;
		public int Row = 3;

		public Vector2 Margin;

		public Vector2 ItemSize;

		private void Awake()
		{
			Reposition();
		}

		public void Reposition()
		{
			var maxChild = transform.childCount;
			var childIndex = 0;

			if (maxChild > 0)
			{
				for (var i = 0; i < Row; i++)
				{
					for (var j = 0; j < Column; j++)
					{
						var child = transform.GetChild(childIndex);
						var position = new Vector2((Margin.y * j + ItemSize.y * j), -(Margin.x * i + ItemSize.x * i));
						child.GetComponent<RectTransform>().anchoredPosition = position;

						childIndex++;
						if (childIndex == maxChild)
						{
							break;
						}
					}

					if (childIndex == maxChild)
					{
						break;
					}
				}
			}
		}
	}
}
