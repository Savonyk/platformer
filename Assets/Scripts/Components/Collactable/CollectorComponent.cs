using UnityEngine;
using Scripts.Model;
using Scripts.Model.Data;
using System.Collections.Generic;

namespace Scripts.Components.Collactable
{
    public class CollectorComponent : MonoBehaviour, ICanAddInInventory
    {
        [SerializeField] 
        private List<InventoryItemData> _items = new ();

        public void AddInInventory(string id, int value)
        {
            _items.Add(new InventoryItemData(id) { Value = value });
        }

        public void DropInInventory()
        {
            var session = FindObjectOfType<GameSession>();

            foreach (var item in _items)
            {
                session.Data.Inventory.Add(item.Id, item.Value);
            }

            _items.Clear();
        }
    }
}
