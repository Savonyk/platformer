using UnityEngine;
using Scripts.Model.Data;
using Scripts.Utils.Disposables;
using UnityEngine.SceneManagement;

namespace Scripts.Model
{
    class GameSession : MonoBehaviour
    {

       [SerializeField] 
        private PlayerData _data;

        private PlayerData _savedData;
        private readonly CompositeDisposable _trash = new ();

        public PlayerData Data => _data;
        public QuickInventoryModel QuickInventory { get; private set; }

        private void Awake()
        {
            LoadHUD();
            if (IsSessionExit())
            {
                Destroy(gameObject);
            }
            else
            {
                Save();
                InitInventoryModels();
                DontDestroyOnLoad(this);
            }
        }

        private void LoadHUD()
        {
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        }

        public void Save()
        {
            _savedData = _data.Clone();
        }

        private void InitInventoryModels()
        {
            QuickInventory = new QuickInventoryModel(Data);
            _trash.Retain(QuickInventory);
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

        public void LoadLastSave()
        {
            _trash.Dispose();
            InitInventoryModels();
            _data = _savedData.Clone();
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
