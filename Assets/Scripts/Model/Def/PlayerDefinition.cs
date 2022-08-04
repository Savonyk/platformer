using UnityEngine;

namespace Scripts.Model.Def
{
    [CreateAssetMenu(menuName = "Definition/PlayerDefinition", fileName = "PlayerDefinition")]
    public class PlayerDefinition : ScriptableObject
    {
        [SerializeField] private int _inventorySize;
        public int InventorySize => _inventorySize;
    }
}
