using UnityEngine;
using System;

namespace Scripts.Model.Def.Repository.Items
{
    [CreateAssetMenu(menuName = "Definition/ThrowableItems", fileName = "ThrowableItems")]
    public class ThrowableIRepository : RepositoryDefinition<ThrowableItemDefinition>
    {
       
    }

    [Serializable]
    public struct ThrowableItemDefinition : IHaveId
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
