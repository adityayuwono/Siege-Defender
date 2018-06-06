using Scripts.Models;
using UnityEngine;

namespace Scripts.Contexts
{
	public class MenuContext : BaseContext
	{
		private void Start()
		{
			Physics.IgnoreLayerCollision(9, 9); // Layer 9 will not collide with layer 9, this is the projectiles

			var engineModel = DataContext.EngineModel;

			var sceneModel = engineModel.Scenes.Find(s => s.Id == gameObject.name);
			var sceneRootModel = new MenuRootModel {SceneModel = sceneModel};
			var sceneRoot = new MenuRoot(sceneRootModel, this);
			sceneRoot.Activate();
			sceneRoot.Show();
		}
	}
}