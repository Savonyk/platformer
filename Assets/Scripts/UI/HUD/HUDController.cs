using UnityEngine;
using Scripts.UI.Widgets;
using Scripts.Model;
using Scripts.Model.Def;

namespace Scripts.UI.HUD
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _progressBar;

        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();

            _session.Data.Health.OnChanged += OnHealthChanged;
            OnHealthChanged(_session.Data.Health.Value, _session.Data.Health.Value);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefinitionFacade.Instance.Player.MaxHealth;
            var value = (float)newValue / maxHealth;
            _progressBar.SetProgress(value);
        }

        private void OnDestroy()
        {
            _session.Data.Health.OnChanged -= OnHealthChanged;
        }
    }
}
