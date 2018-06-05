using UnityEngine;

namespace Scripts.Interfaces
{
	public interface IResource
	{
		GameObject GetGameObject(string assetId);
	}
}