using System;

namespace Scripts.Interfaces
{
	public interface IIntervalRunner
	{
		void SubscribeToInterval(Action action, float delay = 0f, bool startImmediately = true, bool oneTimeTrigger = false);
		void SubscribeOneTimeInterval(Action action, float delay = 0f, bool startImmediately = true);

		bool UnsubscribeFromInterval(Action action);
		void UpdateTime(float timeElapsed);
	}
}