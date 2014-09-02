using System;
using System.Collections;
using Scripts.Helpers;
using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class IntervalView : ElementView
    {
        private readonly IntervalViewModel _viewModel;

        public IntervalView(IntervalViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected virtual void IntervalInvoked()
        {
            throw new NotImplementedException();
        }

        private float _lastIntervalTime = float.NegativeInfinity;


        private int _intervalCount;// BUG: To make sure there's only one interval running
        public void StartInterval()
        {
            if (!_isRunning)
            {
                _isRunning = true;

                if (Time.realtimeSinceStartup > _lastIntervalTime + _viewModel.Interval)
                    _viewModel.Root.StartCoroutine(DoInterval(_viewModel.Interval));
            }
        }

        public void StopInterval()
        {
            _isRunning = false;
        }

        private bool _isRunning;
        private IEnumerator DoInterval(float interval)
        {
            _intervalCount++;
            if (_intervalCount > 1)
            {
                _intervalCount--;
                yield break;
            }

            while (_isRunning)
            {
                _lastIntervalTime = Time.realtimeSinceStartup;

                IntervalInvoked();

                yield return new WaitForSeconds(interval);
            }

            _intervalCount--;
        }
    }
}
