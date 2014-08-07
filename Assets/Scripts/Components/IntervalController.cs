using System;
using System.Collections;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class IntervalController : BaseController
    {
        private float _interval;
        protected override void OnSetup()
        {
            base.OnSetup();

            var viewModel = View as IntervalViewModel;
            _interval = viewModel.Interval;
        }

        public Action OnInterval;

        private float _lastIntervalTime = float.NegativeInfinity;

        public void StartInterval()
        {
            if (!_isRunning)
            {
                _isRunning = true;

                if (Time.realtimeSinceStartup > _lastIntervalTime + _interval)
                    StartCoroutine(DoInterval(_interval));
            }
        }

        public void StopInterval()
        {
            _isRunning = false;
        }

        private bool _isRunning;
        private IEnumerator DoInterval(float interval)
        {
            while (_isRunning)
            {
                _lastIntervalTime = Time.realtimeSinceStartup;

                if (OnInterval != null)
                    OnInterval();

                yield return new WaitForSeconds(interval);
            }
        }

        public override void ClearEvents()
        {
            base.ClearEvents();

            OnInterval = null;
        }
    }
}
