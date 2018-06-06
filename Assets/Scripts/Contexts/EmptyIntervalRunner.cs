using System;
using Scripts.Interfaces;

namespace Scripts.Contexts
{
	public class EmptyIntervalRunner : IIntervalRunner
	{
		public void SubscribeToInterval(Action action, float delay = 0, bool startImmediately = true)
		{
		}

		public bool UnsubscribeFromInterval(Action action)
		{
			return true;
		}

		public void UpdateTime(float timeElapsed)
		{
		}
	}
}