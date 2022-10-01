using UnityEngine;
using Scripts.Components.LevelManagment;
using System.Collections.Generic;
using System.Linq;
using Scripts.Components.Health;

namespace Scripts.Components.GameObjectsBased
{
    class TrapsManager : MonoBehaviour
    {
        [SerializeField] 
        private List<Trap> _traps;
        [SerializeField] 
        private ResetComponent _fireDelay;

        private int _currentTrapIndex;

        private void Start()
        {
            foreach (var trap in _traps)
            {
                trap.enabled = false;
                var healthComponent = trap.GetComponent<HealthComponent>();
                healthComponent._onDeath.AddListener(() => OnTrapDead(trap));
            }
            _currentTrapIndex = 0;
        }

        private void OnTrapDead(Trap trap)
        {
            var index = _traps.IndexOf(trap);
            _traps.Remove(trap);

            if(index < _currentTrapIndex)
            {
                _currentTrapIndex--;
            }
        }

        private void Update()
        {
            if(_traps.Count == 0)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }

            var hasAnyTarget = _traps.Any(trap => trap.IsTouchingPlayer);
            if (!hasAnyTarget || !_fireDelay.IsReady) return;

            _traps[_currentTrapIndex].Shoot();
            _fireDelay.Reset();
            _currentTrapIndex = (int)Mathf.Repeat(_currentTrapIndex + 1, _traps.Count);
        }

    }
}
