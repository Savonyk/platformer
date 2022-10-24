using System;
using UnityEngine;

namespace Scripts.Model.Def.Repository.Items
{
    [CreateAssetMenu(menuName = "Definition/Potions", fileName = "Potions")]
    public class PotionRepository : RepositoryDefinition<PotionDefinition>
    {
    }

    [Serializable]
    public struct PotionDefinition : IHaveId
    {
        [StringInventoryItem] [SerializeField] 
        private string _id;
        [SerializeField]
        private PotionEffect _effect;
        [SerializeField]
        private float _value;
        [SerializeField]
        private float _time;

        public string Id => _id;
        public PotionEffect Effect => _effect;
        public float Value => _value;
        public float Time => _time;
    }


    public enum PotionEffect{
        AddHealth,
        SpeedUp
    }
}
