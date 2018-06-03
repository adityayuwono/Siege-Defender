using Scripts.Models.Enemies;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels
{
	public class StaticEnemy : LivingObject
	{
		public StaticEnemy(StaticEnemyModel model, Base parent) : base(model, parent)
		{

		}

		protected override void OnKilled()
		{
			base.OnKilled();

			Hide("Killed");// Start the hiding process when the enemy is killed
		}
	}
}
