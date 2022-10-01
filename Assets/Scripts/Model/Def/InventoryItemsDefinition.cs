using UnityEngine;
using System;
using System.Linq;

namespace Scripts.Model.Def
{
    [CreateAssetMenu(menuName = "Definition/InventorryItems", fileName = "InventorryItems")]
    public class InventoryItemsDefinition : ScriptableObject
    {

        [SerializeField] 
        private ItemDefinition[] _items;

        public ItemDefinition GetItem(string id)
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
        [SerializeField] 
        private string _id;
        [SerializeField] 
        private ItemTagDefinition[] _tags;
        [SerializeField] 
        private Sprite _icon;

        public string Id => _id;

        public Sprite Icon => _icon;

        public bool IsVoid => string.IsNullOrEmpty(_id);

        public bool HasTag(ItemTagDefinition tag)
        {
            return _tags.Contains(tag);
        }
    }
}
