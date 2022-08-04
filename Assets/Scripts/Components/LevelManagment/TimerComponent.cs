using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Components.LevelManagment
{
    public class TimerComponent : MonoBehaviour
    {
        [SerializeField] private TimerData[] _timers;

        public void SetTimer(int index)
        {
            var timer = _timers[index];
            StartCoroutine(StartTimer(timer));
        }

        private IEnumerator StartTimer(TimerData timer)
        {
            yield return new WaitForSeconds(timer.Delay);
            timer.OnTimesUp?.Invoke();
        }

        [Serializable]
        public class TimerData
        {
            [SerializeField] private float _delay;
            [SerializeField] private UnityEvent _onTimesUp;

            public float Delay { get { return _delay; } set { _delay = value; } }
            public UnityEvent OnTimesUp { get { return _onTimesUp; } set { _onTimesUp = value; } }
        }
    }
}
