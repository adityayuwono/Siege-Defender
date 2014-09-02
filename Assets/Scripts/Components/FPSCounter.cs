﻿using UnityEngine;

namespace Scripts.Components
{
    public class FPSCounter : MonoBehaviour
    {
        private float _updateDelay = 1;
        private int _frameRateCount = 0;
        private float _totalFrameRate = 0;

        private float _averageFPS;

        private void Update()
        {
            _updateDelay -= Time.deltaTime;
            _frameRateCount++;
            _totalFrameRate += Time.deltaTime;

            if (_updateDelay < 0)
            {
                _averageFPS = _frameRateCount/_totalFrameRate;
                _updateDelay = 1f;
                _frameRateCount = 0;
                _totalFrameRate = 0;
            }
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(0,0,100f,25f), _averageFPS.ToString());
        }
    }
}
