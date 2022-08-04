using System;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Model.Def;

namespace Scripts.Model.Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _inventory = new ();

        public delegate void OnInventoryChanged(string id, int value);

        public OnInventoryChanged OnChanged;

        public void Add(string id, int value)
        {

            if (value <= 0) return;

            var itemDefinition = DefinitionFacade.Instance.Items.Get(id);

            if (itemDefinition.IsVoid) return;

            if (itemDefinition.IsStacked)
            {
                AddToStack(id, value);
            }
            else
            {
                AddNonStac(id, value);
            }

            OnChanged?.Invoke(id, ItemCount(id));
        }

        private void AddToStack(string id, int value)
        {
            var item = GetItem(id);

            if (item == null)
            {
                if (IsFullInventory()) return;

                item = new InventoryItemData(id);
                _inventory.Add(item);
            }

            item.Value += value;
        }

        private void AddNonStac(string id, int value)
        {
            for (int i = 0; i < value; i++)
            {
                if (IsFullInventory()) return;

                var item = new InventoryItemData(id) { Value = 1 };
                _inventory.Add(item);
            }
        }

        private bool IsFullInventory()
        {
            return _inventory.Count >= DefinitionFacade.Instance.Player.InventorySize;
        }

        public void Remove(string id, int value)
        {
            var itemDefinition = DefinitionFacade.Instance.Items.Get(id);

            if (itemDefinition.IsVoid) return;

            if (itemDefinition.IsStacked)
            {
                RemoveFromStack(id, value);
            }
            else
            {
                RemoveNonStack(id, value);
            }
            OnChanged?.Invoke(id, ItemCount(id));
        }

        private void RemoveFromStack(string id, int value)
        {
            var item = GetItem(id);
            if (item == null) return;

            item.Value -= value;

            if (item.Value <= 0)
            {
                _inventory.Remove(item);
            }
        }

        private void RemoveNonStack(string id, int value)
        {
            for (int i = 0; i < value; i++)
            {
                var item = GetItem(id);
                if (item == null) return;

                _inventory.Remove(item);
            }
        }

        public int ItemCount(string id)
        {
            var count = 0;

            foreach (var item in _inventory)
            {
                if(item.Id == id)
                {
                    count += item.Value;
                }
            }

            return count;
        }

        private InventoryItemData GetItem(string id)
        {
            foreach (var item in _inventory)
            {
                if(item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

    }

    [Serializable]
    public class InventoryItemData
    {
        [StringInventoryItem] public string Id;
        public int Value;

        public InventoryItemData(string id)
        {
            Id = id;
        }
    }
}
