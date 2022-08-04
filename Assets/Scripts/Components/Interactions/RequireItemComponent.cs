using UnityEngine;
using UnityEngine.Events;
using Scripts.Model.Data;
using Scripts.Model;

namespace Scripts.Components.Interactions
{
    class RequireItemComponent : MonoBehaviour
    {
        [SerializeField] private InventoryItemData[] _requiredItems;
        [SerializeField] private bool _removeAfterUse;

        [SerializeField] private UnityEvent _OnSuccessed;
        [SerializeField] private UnityEvent _OnFailed;

        public void Check()
        {
            var session = FindObjectOfType<GameSession>();
            var areAllRequirementsMet = true;
            foreach (var item in _requiredItems)
            {
                var numItems = session.Data.Inventory.ItemCount(item.Id);
                if(numItems < item.Value)
                {
                    areAllRequirementsMet = false;
                   
                }
            }

            if(areAllRequirementsMet)
            {
                if (_removeAfterUse)
                {
                    foreach (var item in _requiredItems)
                    {
                        session.Data.Inventory.Remove(item.Id, item.Value);
                    }
                }
                _OnSuccessed?.Invoke();
            }
            else
            {
                _OnFailed?.Invoke();
            }


        }
    }
}
