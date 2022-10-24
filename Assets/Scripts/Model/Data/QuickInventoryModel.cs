using System;
using UnityEngine;
using Scripts.Model.Def;
using Scripts.Utils.Disposables;
using Scripts.Model.Def.Repository;
using Scripts.Model.Data.Properties;
using Scripts.Model.Def.Repository.Items;

namespace Scripts.Model.Data
{
    public class QuickInventoryModel : IDisposable
    {
        private readonly PlayerData _data;

        public readonly IntObservableProperty SelectedIndex = new();

        public InventoryItemData[] Inventory { get; private set; }

        public InventoryItemData SelectedItem
        {
            get
            {
                return (Inventory.Length > 0 && Inventory.Length > SelectedIndex.Value) ?
                    Inventory[SelectedIndex.Value] : null;
            }
        }

        public ItemDefinition SelectedDefinition => DefinitionFacade.Instance.Items.GetItem(SelectedItem?.Id);

        public event Action OnChanged;

        public QuickInventoryModel(PlayerData data)
        {
            _data = data;

            Inventory = _data.Inventory.GetArrayItems(ItemTagDefinition.Usable);
            _data.Inventory.OnChanged += OnInventoryChanged;
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        private void OnInventoryChanged(string id, int value)
        {
            var indexFound = Array.FindIndex(Inventory, x => x.Id == id);

            Inventory = _data.Inventory.GetArrayItems(ItemTagDefinition.Usable);
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
            OnChanged?.Invoke();
        }

        public void SetNextItem()
        {
            SelectedIndex.Value = (int)Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);
        }

        public void Dispose()
        {
            _data.Inventory.OnChanged -= OnInventoryChanged;
        }
    }
}
