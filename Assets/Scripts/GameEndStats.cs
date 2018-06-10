using System;
using UnityEngine;

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
			_totalDamage = 0;
			_totalTime = 0;
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
