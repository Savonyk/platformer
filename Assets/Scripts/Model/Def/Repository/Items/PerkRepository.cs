using System;
using Scripts.Components.LevelManagment;
using UnityEngine;

namespace Scripts.Model.Def.Repository.Items
{
    [CreateAssetMenu(menuName = "Definition/Repository/Perks", fileName = "Perks")]
    public class PerkRepository : RepositoryDefinition<PerkDefinition>
    {
    }

    [Serializable]
    public struct PerkDefinition : IHaveId
    {
        [SerializeField]
        private string _id;
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private string _textInfo;
        [SerializeField]
        private float _coolDownTime;
        [SerializeField]
        private ItemWithCount _price;

        public string Id => _id;
        public Sprite Icon => _icon;
        public string Info => _textInfo;
        public float CoolDownTime => _coolDownTime;
        public ItemWithCount Price => _price;
    }

    [Serializable]
    public struct ItemWithCount
    {
        [StringInventoryItem]
        [SerializeField]
        private string _itemId;
        [SerializeField]
        private int _count;

        public string ItemId => _itemId;
        public int Count => _count;
    }
}
