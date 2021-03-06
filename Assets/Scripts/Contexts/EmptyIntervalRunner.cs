using System;
using Scripts.Interfaces;

namespace Scripts.Contexts
{
	public class EmptyIntervalRunner : IIntervalRunner
	{
		public void SubscribeToInterval(Action action, float delay = 0, bool startImmediately = true, bool oneTimeTrigger = false)
		{
			action();
		}

		public void SubscribeOneTimeInterval(Action action, float delay = 0, bool startImmediately = true)
		{
			action();
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