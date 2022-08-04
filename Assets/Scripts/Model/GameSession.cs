using UnityEngine;
using Scripts.Model.Data;

namespace Scripts.Model
{
    class GameSession : MonoBehaviour
    {

       [SerializeField] private PlayerData _data;

        public PlayerData Data => _data;
        private PlayerData _savedData;

        private void Awake()
        {
            if (IsSessionExit())
            {
                Destroy(gameObject);
            }
            else
            {
                Save();
                DontDestroyOnLoad(this);
            }
        }

        private bool IsSessionExit()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var session in sessions)
            {
                if (session != this) return true;
            }
            return false;
        }

        public void Save()
        {
            _savedData = _data.Clone();
        }

        public void LoadLastSave()
        {
            _data = _savedData.Clone();
        }
    }
}
