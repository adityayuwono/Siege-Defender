using Scripts.Components;
using Scripts.Models;

namespace Scripts.Contexts
{
	public class MenuContext : BaseContext
	{
		protected override void Start()
		{
			base.Start();

			// Prepare the IntervalRunner, this will manage all time based execution of this game
			IntervalRunner = gameObject.AddComponent<IntervalRunner>();

			var engineModel = DataContext.Instance.EngineModel;

			var sceneModel = engineModel.Scenes.Find(s => s.Id == gameObject.name);
			var sceneRootModel = new MenuRootModel {SceneModel = sceneModel};
			var sceneRoot = new MenuRoot(sceneRootModel, this);
			sceneRoot.Activate();
			sceneRoot.Show();
		}
	}
}