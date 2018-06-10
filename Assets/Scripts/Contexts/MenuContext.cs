using Scripts.Models;
using UnityEngine;

namespace Scripts.Contexts
{
	public class MenuContext : BaseContext
	{
		private void Start()
		{
			var engineModel = DataContext.EngineModel;

			var sceneModel = engineModel.Scenes.Find(s => s.Id == gameObject.name);
			var sceneRootModel = new MenuRootModel {SceneModel = sceneModel};
			var sceneRoot = new MenuRoot(sceneRootModel, this);
			sceneRoot.Activate();
			sceneRoot.Show();
		}
	}
}