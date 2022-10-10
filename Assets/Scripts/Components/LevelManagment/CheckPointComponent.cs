using UnityEngine;
using UnityEngine.Events;
using Scripts.Components.GameObjectsBased;
using Scripts.Model;

namespace Scripts.Components.LevelManagment
{
    class CheckPointComponent : MonoBehaviour
    {
        [SerializeField]
        private string _id;
        [SerializeField]
        private SpawnComponent _heroSpawner;
        [SerializeField]
        private UnityEvent _setUnchecked;
        [SerializeField]
        private UnityEvent _setChecked;

        private GameSession _gameSession;

        public string Id => _id;

        private void Start()
        {
            _gameSession = FindObjectOfType<GameSession>();

            if (_gameSession.IsChecked(_id))
            {
                _setChecked?.Invoke();
            }
            else
            {
                _setUnchecked?.Invoke();
            }
        }

        public void Check()
        {
            _setChecked?.Invoke();
            _gameSession.SetChecked(_id);
        }

        public void SpawnHero()
        {
            _heroSpawner.Spawn();
        }

    }
}
