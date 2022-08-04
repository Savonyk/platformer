using UnityEngine;
using Scripts.Model.Def;
using Scripts.Utils;
using Scripts.Model.Data;

namespace Scripts.Components.Collactable
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [StringInventoryItem] [SerializeField] private string _id;
        [SerializeField] private int _value;

        public void Add(GameObject target)
        {
            var canAddInInventoryInterface = target.GetInterface<ICanAddInInventory>();
            canAddInInventoryInterface?.AddInInventory(_id, _value);
        }
    }
}
