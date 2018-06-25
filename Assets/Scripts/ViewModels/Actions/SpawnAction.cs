using Scripts.Models.Actions;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels.Actions
{
	public class SpawnAction : BaseAction
	{
		private readonly SpawnActionModel _model;

		public SpawnAction(SpawnActionModel model, Base parent) : base(model, parent)
		{
			_model = model;
		}

		public override void Invoke()
		{
			base.Invoke();

			var enemyId = _model.EnemyId;
			if (_model.EnemyId.Contains(";"))
			{
				var enemyIds = _model.EnemyId.Split(';');
				var randomIndex = Root.Randomizer.Next(0, enemyIds.Length);
				enemyId = enemyIds[randomIndex];
			}

			var scene = GetParent<Scene>();
			var enemy = scene.EnemyManager.GetEnemy(enemyId, scene);
			enemy.Activate(GetParent<LivingObject>().Position);
			enemy.Show();
		}
	}
}
