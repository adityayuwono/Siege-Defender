using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Components
{
    /// <summary>
    /// Helper Component to iterate the time
    /// </summary>
    public class IntervalRunner : MonoBehaviour
    {
        private readonly List<IntervalSubscriber> _intervals = new List<IntervalSubscriber>();
        public void SubscribeToInterval(Action action, float delay, bool startImmediately = true)
        {
            if (!IsContainInterval(action))
                _intervals.Add(new IntervalSubscriber(action, delay, startImmediately));
        }

        public bool UnsubscribeFromInterval(Action action)
        {
            var intervalToRemove = GetInterval(action);
            if (intervalToRemove != null)
            {
                _intervals.Remove(intervalToRemove);
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Check if we already have an interval with that spesific action
        /// </summary>
        private bool IsContainInterval(Action action)
        {
            return GetInterval(action) != null;
        }

        private IntervalSubscriber GetInterval(Action action)
        {
            return _intervals.FirstOrDefault(interval => interval.ActionHash == action.GetHashCode());
        }

        private void Update()
        {
            foreach (var interval in _intervals.ToArray())
                interval.Update(Time.deltaTime);
        }

        private class IntervalSubscriber
        {
            public readonly int ActionHash; 
            private readonly Action _onInvokedAction;
            private readonly float _delay;
            private float _currentDelay;

            public IntervalSubscriber(Action action, float delay, bool startImmediatelly)
            {
                ActionHash = action.GetHashCode();

                _onInvokedAction = action;
                _delay = delay;

                if (!startImmediatelly)
                    _currentDelay = _delay;
            }

            public void Update(float deltaTime)
            {
                _currentDelay -= deltaTime;
                if (_currentDelay <= 0)
                {
                    _onInvokedAction();
                    _currentDelay += _delay;
                }
            }
        }
    }
}
