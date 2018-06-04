using System;
using Scripts.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Scripts.Helpers
{
    public class ResourcePooler : IResource
    {
        private readonly RootBase _mainEngine;

        public ResourcePooler(RootBase engine)
        {
            _mainEngine = engine;
        }

        public GameObject GetGameObject(string assetId)
        {
            try
            {
                var asset = Resources.Load<Object>(assetId);
                var newObject = Object.Instantiate(asset) as GameObject;
                newObject.name = asset.name;

                return newObject;
            }
            catch (Exception ex)
            {
                throw new EngineException(_mainEngine, string.Format("Failed to find resource with path: {0}\n{1}", assetId, ex));
            }
        }
    }
}
