using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Model.Def.Repository.Items
{
    public class RepositoryDefinition<TDefinitionType> : ScriptableObject where TDefinitionType : IHaveId
    {
        [SerializeField] 
        protected TDefinitionType[] Collection;

        public TDefinitionType[] GetAll => new List<TDefinitionType>(Collection).ToArray();

        public TDefinitionType GetItem(string id)
        {
            if (string.IsNullOrEmpty(id)) return default;

            foreach (var item in Collection)
            {
                if (item.Id == id) return item;
            }

            return default;
        }
    }
}
