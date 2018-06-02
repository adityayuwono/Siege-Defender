using Scripts.Models;
using Scripts.ViewModels.Enemies;

namespace Scripts.ViewModels
{
	public class StaticEnemy : LivingObject
	{
		public StaticEnemy(StaticEnemyModel model, Base parent) : base(model, parent)
		{

		}
	}
}
