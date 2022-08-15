using System;
using UnityEngine;
using Scripts.Model.Data.Properties;

namespace Scripts.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;
        public IntObservableProperty Health = new ();

        public InventoryData Inventory => _inventory;


        public PlayerData Clone()
        {
            var file = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(file);
        }
    }
}
