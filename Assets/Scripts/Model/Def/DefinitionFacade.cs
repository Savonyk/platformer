using UnityEngine;

namespace Scripts.Model.Def
{
    [CreateAssetMenu(menuName = "Definition/DefinitionFacade", fileName = "DefinitionFacade")]
    public class DefinitionFacade : ScriptableObject
    {
        [SerializeField] 
        private InventoryItemsDefinition _items;
        [SerializeField] 
        private PlayerDefinition _player;
        [SerializeField] 
        private ThrowableItemsDefinition _throwableItems;

        public InventoryItemsDefinition Items => _items;
        public PlayerDefinition Player => _player;
        public ThrowableItemsDefinition ThrowableItems => _throwableItems;

        private static DefinitionFacade _instance;

        public static DefinitionFacade Instance => _instance == null ? LoadDefinition() : _instance;

        private static DefinitionFacade LoadDefinition()
        {
            return _instance = Resources.Load<DefinitionFacade>("DefinitionFacade");
        }
    }
}
