namespace Scripts
{
	public class GameEndStats
	{
		private static int TotalHit;
		private static int TotalProjectileShot;

		public static void Reset()
		{
			TotalHit = 0;
			TotalProjectileShot = 0;
			EnemiesKilled = 0;
		}

		public static void AddOneProjectile()
		{
			TotalProjectileShot++;
		}

		public static void AddOneHit()
		{
			TotalHit++;
		}

		public static float GetAccuracy()
		{
			return TotalHit * 100f / TotalProjectileShot;
		}

		private static int EnemiesKilled;

		public static void AddOneEnemyKilled()
		{
			EnemiesKilled++;
		}

		public static object GetEnemiesKilled()
		{
			return EnemiesKilled;
		}
	}
}
