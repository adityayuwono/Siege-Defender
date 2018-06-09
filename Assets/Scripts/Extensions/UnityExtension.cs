using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Extensions
{
	public static class UnityExtension
	{
		/// <summary>
		///     Will lookup the child recursively, using breadth first search
		/// </summary>
		/// <param name="name">The name of the child GameObjects you wish to find</param>
		/// <param name="includeInactive">Whether to include inactivate GameObjects or not</param>
		/// <param name="value">The parent transform</param>
		/// <returns></returns>
		public static Transform FindChildRecursivelyBreadthFirst(this Transform value, string name,
			bool includeInactive = true)
		{
			var splitNames = name.Split('/');

			var queue = new List<Transform>();
			var child = value;

			for (var i = 0; i < child.childCount; i++)
				queue.Add(child.GetChild(i));

			while (queue.Count > 0)
			{
				child = queue[0];
				queue.RemoveAt(0);

				var childSearchPattern = splitNames[splitNames.Length - 1];

				var isFound = child.name == childSearchPattern;
				isFound &= includeInactive || child.gameObject.activeInHierarchy;

				if (isFound) return child;

				for (var i = 0; i < child.childCount; i++)
					queue.Add(child.GetChild(i));
			}

			return null;
		}

		public static Vector3 ParseVector3(this string string3)
		{
			var splitted = string3.Split(',');
			var splitFloat = new float[splitted.Length];

			for (var i = 0; i < splitted.Length; i++) splitFloat[i] = float.Parse(splitted[i]);

			return new Vector3(splitFloat[0], splitFloat[1], splitFloat[2]);
		}

		public static T AddMissingComponent<T>(this GameObject gameObject) where T : Component
		{
			var component = gameObject.GetComponent<T>();
			if (component == null)
			{
				return gameObject.AddComponent<T>();
			}
			else
			{
				return component;
			}
		}

		public static bool ContainsIfThisIsACircle(this Rect rect, Vector2 point)
		{
			var center = rect.center;
			var radius = (rect.width + rect.height) / 4f;

			var distanceFromPointToCenter = Vector2.Distance(point, center);

			return distanceFromPointToCenter <= radius;
		}
	}
}