using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Model.Data;
using Scripts.Utils.Disposables;
using Scripts.Components.LevelManagment;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Model
{
    class GameSession : MonoBehaviour
    {
        [SerializeField]
        private string _defaultCheckpointId;
        [SerializeField] 
        private PlayerData _data;

        private PlayerData _savedData;
        private readonly List<string> _checkPoints = new ();
        private readonly CompositeDisposable _trash = new ();

        public PlayerData Data => _data;
        public QuickInventoryModel QuickInventory { get; private set; }
        public PerksModel PerksModel { get; private set; }

        private void Awake()
        {
            if (TryGetExistingSession(out GameSession existedSession))
            {
                existedSession.StartSession(_defaultCheckpointId);
                Destroy(gameObject);
            }
            else
            {
                Save();
                InitInventoryModels();
                DontDestroyOnLoad(this);
                StartSession(_defaultCheckpointId);
            }
        }

        private bool TryGetExistingSession(out GameSession savedSession)
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var session in sessions)
            {
                if (session != this)
                {
                    savedSession = session;
                    return true;
                }
            }
            savedSession = null;
            return false;
        }

        private void StartSession(string defaultCheckPointId)
        {
            SetChecked(defaultCheckPointId);
            LoadHUD();
            SpawnHero();
        }

        public void Save()
        {
            _savedData = _data.Clone();
        }


        private void LoadHUD()
        {
            SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
        }

        private void SpawnHero()
        {
            var checkPoints = FindObjectsOfType<CheckPointComponent>();
            var lastCheckPoint = _checkPoints.Last();
            foreach (var checkPoint in checkPoints)
            {
                if(checkPoint.Id == lastCheckPoint)
                {
                    checkPoint.SpawnHero();
                    break;
                }
            }
        }

        private void InitInventoryModels()
        {
            QuickInventory = new QuickInventoryModel(_data);
            _trash.Retain(QuickInventory);

            PerksModel = new PerksModel(_data);
            _trash.Retain(PerksModel);
        }

        public void LoadLastSave()
        {
            _data = _savedData.Clone();

            _trash.Dispose();

            InitInventoryModels();
        }

        public bool IsChecked(string id)
        {
            return _checkPoints.Contains(id);
        }

        public void SetChecked(string id)
        {
            if (IsChecked(id)) return;

            _checkPoints.Add(id);
             Save();

        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
