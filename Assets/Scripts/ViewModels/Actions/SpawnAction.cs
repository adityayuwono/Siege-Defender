using Scripts.Models.Actions;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels.Actions
{
	public class SpawnAction : BaseAction
	{
		private SpawnActionModel _model;
		public SpawnAction(SpawnActionModel model, Base parent) : base(model, parent)
		{
			_model = model;
		}

		public override void Invoke()
		{
			base.Invoke();

			var scene = GetParent<Scene>();
			var enemy = scene.EnemyManager.GetEnemy(_model.EnemyId, scene);
			enemy.Activate(GetParent<LivingObject>().Position);
			enemy.Show();
		}
	}
}
