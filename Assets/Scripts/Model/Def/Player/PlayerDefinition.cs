using UnityEngine;
using System.Linq;

namespace Scripts.Model.Def.Player
{
    [CreateAssetMenu(menuName = "Definition/PlayerDefinition", fileName = "PlayerDefinition")]
    public class PlayerDefinition : ScriptableObject
    {
        [SerializeField] 
        private int _inventorySize;
        [SerializeField] 
        private int _maxHealth;
        [SerializeField]
        private int _maxPerksCount;
        [SerializeField]
        private StatDefinition[] _stats;

        public int InventorySize => _inventorySize;
        public int MaxHealth => _maxHealth;
        public int MaxPerksCount => _maxPerksCount;
        public StatDefinition[] Stats => _stats;

        public StatDefinition GetStat(StatId id) => _stats.FirstOrDefault(item => item.Id == id);
    }
}
