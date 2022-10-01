using UnityEngine;
using System;
using UnityEngine.Events;

namespace Scripts.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] 
        private int _health;
        [SerializeField] 
        private UnityEvent _onDamage;
        [SerializeField] 
        private UnityEvent _onHeal;


        public ChangeHealthEvent _onChange;
        public UnityEvent _onDeath;
        public int Health { get => _health; set => _health = value; }

        public void ModifyHealth(int healthDelta)
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
        public class ChangeHealthEvent : UnityEvent<int> {}
    }
}