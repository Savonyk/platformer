using UnityEngine;
using Scripts.Model.Def.Repository.Items;

namespace Scripts.Model.Def
{
    [CreateAssetMenu(menuName = "Definition/DefinitionFacade", fileName = "DefinitionFacade")]
    public class DefinitionFacade : ScriptableObject
    {
        [SerializeField] 
        private ItemsRepository _items;
        [SerializeField] 
        private PlayerDefinition _player;
        [SerializeField] 
        private ThrowableIRepository _throwableItems;
        [SerializeField]
        private PotionRepository _potions;
        [SerializeField]
        private PerkRepository _perks;

        public ItemsRepository Items => _items;
        public PlayerDefinition Player => _player;
        public ThrowableIRepository ThrowableItems => _throwableItems;
        public PotionRepository Potions => _potions;
        public PerkRepository Perks => _perks;

        private static DefinitionFacade _instance;

        public static DefinitionFacade Instance => _instance == null ? LoadDefinition() : _instance;

        private static DefinitionFacade LoadDefinition()
        {
            return _instance = Resources.Load<DefinitionFacade>("DefinitionFacade");
        }
    }
}
