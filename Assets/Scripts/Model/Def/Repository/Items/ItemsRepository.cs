using UnityEngine;
using System;
using System.Linq;

namespace Scripts.Model.Def.Repository.Items
{
    [CreateAssetMenu(menuName = "Definition/Items", fileName = "Items")]
    public class ItemsRepository : RepositoryDefinition<ItemDefinition>
    {

#if UNITY_EDITOR
        public ItemDefinition[] ItemsForEditor => Collection;
#endif

    }

    [Serializable]
    public struct ItemDefinition : IHaveId
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
            return _tags?.Contains(tag) ?? false;
        }
    }
}
