using UnityEngine;
using Scripts.Model;
using Scripts.Utils.Disposables;
using Scripts.Model.Data;
using System.Collections.Generic;
using Scripts.UI.Widgets;

namespace Scripts.UI.HUD.QuickInventory
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] 
        private Transform _container;
        [SerializeField] 
        private InventoryItemWidget _prefab;

        private readonly CompositeDisposable _trash = new();
        private GameSession _session;
        private DataGroup<InventoryItemData, InventoryItemWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<InventoryItemData, InventoryItemWidget>(_prefab, _container);
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.QuickInventory.Subscribe(Rebuild));
            Rebuild();
        }

        private void Rebuild()
        {
            var inventory = _session.QuickInventory.Inventory;

            _dataGroup.SetData(inventory);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
