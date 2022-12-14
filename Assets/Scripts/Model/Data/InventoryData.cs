using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Model.Def;
using Scripts.Model.Def.Repository;
using Scripts.Model.Def.Repository.Items;

namespace Scripts.Model.Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] 
        private List<InventoryItemData> _inventory = new ();

        public delegate void OnInventoryChanged(string id, int value);

        public OnInventoryChanged OnChanged;

        public void Add(string id, int value)
        {

            if (value <= 0) return;

            var itemDefinition = DefinitionFacade.Instance.Items.GetItem(id);

            if (itemDefinition.IsVoid) return;

            if (itemDefinition.HasTag(ItemTagDefinition.Stackable))
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
            var itemDefinition = DefinitionFacade.Instance.Items.GetItem(id);

            if (itemDefinition.IsVoid) return;

            if (itemDefinition.HasTag(ItemTagDefinition.Stackable))
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

        public InventoryItemData[] GetArrayItems(params ItemTagDefinition[] tags)
        {
            var returnedValue = new List<InventoryItemData>();

            foreach (var item in _inventory)
            {
                var itemDefinition = DefinitionFacade.Instance.Items.GetItem(item.Id);
                var isAllRequirementsMet = tags.All(tag => itemDefinition.HasTag(tag));
                if (isAllRequirementsMet)
                {
                    returnedValue.Add(item);
                }
            }

            return returnedValue.ToArray();
        }

        public bool IsEnoughResources(params ItemWithCount[] items)
        {
            var joined = new Dictionary<string, int>();

            foreach (var item in items)
            {
                if (joined.ContainsKey(item.ItemId))
                {
                    joined[item.ItemId] += item.Count;
                }
                else
                {
                    joined.Add(item.ItemId, item.Count);
                }
            }

            foreach (var item in joined)
            {
                var count = ItemCount(item.Key);
                if (count < item.Value) return false;
            }

            return true;
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
