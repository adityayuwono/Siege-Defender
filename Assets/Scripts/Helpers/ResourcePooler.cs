using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Helpers
{
    public class ResourcePooler : IResource
    {
        public GameObject GetGameObject(string assetId)
        {
            var asset = Resources.Load<GameObject>(assetId);
            var newObject = Object.Instantiate(asset) as GameObject;
            newObject.name = asset.name;

            return newObject;
        }
    }
}
