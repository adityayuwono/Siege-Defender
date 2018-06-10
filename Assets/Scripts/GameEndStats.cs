using System;
using UnityEngine;

namespace Scripts
{
	public class GameEndStats
	{
		private static int _totalHit;
		private static int _totalProjectileShot;

		public static void Reset()
		{
			_totalHit = 0;
			_totalProjectileShot = 0;
			_enemiesKilled = 0;
			_totalDamage = 0;
			_totalTime = 0;
		}

		public static void AddOneProjectile()
		{
			_totalProjectileShot++;
		}

		public static void AddOneHit()
		{
			_totalHit++;
		}

		public static float GetAccuracy()
		{
			return _totalHit * 100f / _totalProjectileShot;
		}

		private static int _enemiesKilled;

		public static void AddOneEnemyKilled()
		{
			_enemiesKilled++;
		}

		public static object GetEnemiesKilled()
		{
			return _enemiesKilled;
		}

		private static float _totalDamage;
		private static float _totalTime;
		private static float _timeOflastDamage;

		public static void AddDamage(float damage)
		{
			var timeSinceLastDamage = Time.time - _timeOflastDamage;
			if (Math.Abs(_totalDamage) > float.Epsilon)
			{
				timeSinceLastDamage = 0;
			}
			_totalTime += timeSinceLastDamage;
			_timeOflastDamage = Time.time;

			_totalDamage += damage;
		}

		public static float GetTotalDamage()
		{
			return _totalDamage;
		}

		public static float GetDPS()
		{
			return _totalDamage / _totalTime;
		}
	}
}
