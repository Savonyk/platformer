using System;
using UnityEngine;

namespace Scripts.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;
        [SerializeField] private float _health;

        public InventoryData Inventory => _inventory;

        public float Health { get { return _health; } set { _health = value; } }

        public PlayerData Clone()
        {
            var file = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(file);
        }
    }
}
