using UnityEngine;
using System;

namespace Scripts.Model.Def
{
    [CreateAssetMenu(menuName = "Definition/ThrowableItemsDef", fileName = "ThrowableItemsDef")]
    public class ThrowableItemsDefinition : ScriptableObject
    {
        [SerializeField] 
        private ThrowableItemDefinition[] _items;

        public ThrowableItemDefinition GetItem(string id)
        {
            foreach (var item in _items)
            {
                if(item.Id == id)
                {
                    return item;
                }
            }
            return default;
        }
    }

    [Serializable]
    public struct ThrowableItemDefinition
    {
        [StringInventoryItem] [SerializeField] 
        private string _id;
        [SerializeField] 
        private GameObject _projectilePrefab;

        public string Id => _id;
        public GameObject Projectile => _projectilePrefab;

        public bool IsVoid => string.IsNullOrEmpty(_id);
    }
}
