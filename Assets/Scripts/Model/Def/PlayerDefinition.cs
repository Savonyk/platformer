using UnityEngine;

namespace Scripts.Model.Def
{
    [CreateAssetMenu(menuName = "Definition/PlayerDefinition", fileName = "PlayerDefinition")]
    public class PlayerDefinition : ScriptableObject
    {
        [SerializeField] private int _inventorySize;
        [SerializeField] private int _maxHealth;
        public int InventorySize => _inventorySize;
        public int MaxHealth => _maxHealth;
    }
}
