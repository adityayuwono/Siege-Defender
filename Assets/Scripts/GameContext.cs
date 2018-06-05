using Scripts.Components;
using UnityEngine;

namespace Scripts
{
	public class GameContext : BaseContext
	{
		private void Start()
		{
			// Prepare the IntervalRunner, this will manage all time based execution of this game
			IntervalRunner = gameObject.AddComponent<IntervalRunner>();

			Physics.IgnoreLayerCollision(9, 9); // Layer 9 will not collide with layer 9, this is the projectiles

			var engineModel = DataContext.EngineModel;

			var sceneModel = engineModel.Scenes.Find(s => s.Id == gameObject.name);
			engineModel.SceneModel = sceneModel;
			var engineRoot = new GameRoot(engineModel, this);
			engineRoot.Activate();
			engineRoot.Show();
		}
	}
}