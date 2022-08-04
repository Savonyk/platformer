using UnityEngine;
using System;

namespace Scripts.Model.Def
{
    [CreateAssetMenu(menuName = "Definition/InventorryItems", fileName = "InventorryItems")]
    public class InventoryItemsDefinition : ScriptableObject
    {

        [SerializeField] private ItemDefinition[] _items;

        public ItemDefinition Get(string id)
        {
            foreach (var item in _items)
            {
                if (item.Id == id) return item;
            }

            Debug.LogError(id);
            return default;
        }

#if UNITY_EDITOR
        public ItemDefinition[] ItemsForEditor => _items;
#endif

    }

    [Serializable]
    public struct ItemDefinition
    {
        [SerializeField] private string _id;
        [SerializeField] private bool _isStacked;

        public string Id => _id;
        public bool IsStacked => _isStacked;
        public bool IsVoid => string.IsNullOrEmpty(_id);
    }
}
