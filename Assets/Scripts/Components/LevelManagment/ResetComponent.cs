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

        public bool IsReady => Time.time >= _currentTime;
        public void Reset()
        {
            _currentTime = Time.time + _time;
        }
    }
}
