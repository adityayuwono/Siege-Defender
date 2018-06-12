using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Components
{
	/// <summary>
	///     Helper Component to iterate the time
	/// </summary>
	public class IntervalRunner : MonoBehaviour, IIntervalRunner
	{
		private readonly List<IntervalSubscriber> _intervals = new List<IntervalSubscriber>();

		public void SubscribeToInterval(Action action, float delay = 0f, bool startImmediately = true, bool oneTimeTrigger = false)
		{
			if (!IsContainInterval(action))
			{
				Action<IntervalSubscriber> removeSubscriber = null;
				if (oneTimeTrigger)
				{
					removeSubscriber = RemoveInterval;
				}

				_intervals.Add(new IntervalSubscriber(action, delay, startImmediately, removeSubscriber));
			}
			else
			{
#if UNITY_EDITOR
				throw new Exception(string.Format("Multiple registration of {0}", action));
#endif
			}
		}

		public void SubscribeOneTimeInterval(Action action, float delay = 0, bool startImmediately = true)
		{
			SubscribeToInterval(action, delay, startImmediately, true);
		}

		public bool UnsubscribeFromInterval(Action action)
		{
			var intervalToRemove = GetInterval(action);
			if (intervalToRemove != null)
			{
				RemoveInterval(intervalToRemove);
				return true;
			}

			return false;
		}

		public void UpdateTime(float timeElapsed)
		{
			foreach (var interval in _intervals.ToArray())
			{
				interval.Update(timeElapsed);
			}
		}

		private void Update()
		{
			UpdateTime(Time.deltaTime);
		}

		private void RemoveInterval(IntervalSubscriber interval)
		{
			_intervals.Remove(interval);
		}

		/// <summary>
		///     Check if we already have an interval with that specific action
		/// </summary>
		private bool IsContainInterval(Action action)
		{
			return GetInterval(action) != null;
		}

		private IntervalSubscriber GetInterval(Action action)
		{
			return _intervals.FirstOrDefault(interval => interval.ActionHash == action.GetHashCode());
		}

		private class IntervalSubscriber
		{
			private readonly float _delay;
			private readonly Action _onInvokedAction;
			public readonly int ActionHash;
			private float _currentDelay;
			private readonly Action<IntervalSubscriber> _removeInterval;

			public IntervalSubscriber(Action action, float delay, bool startImmediatelly, Action<IntervalSubscriber> removeInterval)
			{
				ActionHash = action.GetHashCode();

				_onInvokedAction = action;
				_delay = delay;
				_removeInterval = removeInterval;

				if (!startImmediatelly)
				{
					_currentDelay = _delay;
				}
			}

			public void Update(float deltaTime)
			{
				_currentDelay -= deltaTime;
				if (_currentDelay <= 0)
				{
					_onInvokedAction();

					if (_removeInterval != null)
					{
						_removeInterval(this);
					}
					_currentDelay += _delay;
				}
			}
		}
	}
}