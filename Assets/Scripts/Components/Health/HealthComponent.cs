using UnityEngine;
using System;
using UnityEngine.Events;

namespace Scripts.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] ChangeHealthEvent _onChange;

        public UnityEvent _onDeath;
        public float Health { get { return _health; } set { _health = value; } }

        public void ModifyHealth(float healthDelta)
        {

            _health += healthDelta;
            _onChange?.Invoke(_health);

            if (healthDelta > 0)
            {
                _onHeal?.Invoke();
            }
            else if(healthDelta < 0)
            {
                _onDamage?.Invoke();
            }

            if (_health <= 0)
            {
                _onDeath?.Invoke();
            }
        }

        private void OnDestroy()
        {
            _onDeath.RemoveAllListeners();
        }

        [Serializable]
        public class ChangeHealthEvent : UnityEvent<float> {}
    }
}