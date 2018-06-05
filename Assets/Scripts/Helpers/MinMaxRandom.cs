using UnityEngine;

namespace Scripts.Helpers
{
	public class MinMaxRandom
	{
		private readonly Collider _collider;

		public MinMaxRandom(Collider collider)
		{
			_collider = collider;
		}

		public Vector3 GetRandomSpot(bool ignoreY = true)
		{
			var min = _collider.bounds.min;
			var max = _collider.bounds.max;
			var rX = Random.Range(min.x, max.x);
			var rY = ignoreY ? 0 : Random.Range(min.y, min.y);
			var rZ = Random.Range(min.z, max.z);

			return new Vector3(rX, rY, rZ);
		}
	}
}