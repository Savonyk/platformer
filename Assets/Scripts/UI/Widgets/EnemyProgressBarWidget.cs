using UnityEngine;
using Scripts.Components.Health;
using Scripts.Utils.Disposables;
using System;

namespace Scripts.UI.Widgets
{
    public class EnemyProgressBarWidget : MonoBehaviour
    {
        [SerializeField]
        private ProgressBarWidget _progressBar;
        [SerializeField]
        private HealthComponent _healthComponent;

        private int _maxHealth;
        private CompositeDisposable _trash = new();

        private void Start()
        {
            if(_healthComponent == null)
            {
                _healthComponent = GetComponentInParent<HealthComponent>();
            }

            _maxHealth = _healthComponent.Health;

            _trash.Retain(_healthComponent._onChange.Subscribe(OnHealthChanged));
            _trash.Retain(_healthComponent._onDeath.Subscribe(OnDie));
        }

        private void OnHealthChanged(int health)
        {
            var healthInPercent = (float)health / _maxHealth;
            _progressBar.SetProgress(healthInPercent);
        }

        private void OnDie()
        {
            Destroy(this);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
