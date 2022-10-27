using UnityEngine;
using System;

namespace Scripts.Components.LevelManagment
{
    [Serializable]
    public class ResetComponent
    {
        [SerializeField] 
        private float _time;

        private float _currentTime;

        public float SetTime(float time) => _time = time;
        public bool IsReady => Time.time >= _currentTime;
        public float RemainingTime => _currentTime - Time.time;

        public void Reset()
        {
            _currentTime = Time.time + _time;
        }
    }
}
