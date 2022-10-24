using UnityEngine;
using Scripts.Model;
using Scripts.UI.Widgets;
using Scripts.Utils.Disposables;

namespace Scripts.UI.HUD.ActivePerks
{
    public class ActivePerksController : MonoBehaviour
    {
        [SerializeField]
        private Transform _container;
        [SerializeField]
        private ActivePerksWidget _prefab;

        private readonly CompositeDisposable _trash = new();
        private GameSession _session;
        private DataGroup<string, ActivePerksWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<string, ActivePerksWidget>(_prefab, _container);
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.PerksModel.Subscribe(Rebuild));
            Rebuild();
        }

        private void Rebuild()
        {
            _dataGroup.SetData(_session.PerksModel.Used);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
