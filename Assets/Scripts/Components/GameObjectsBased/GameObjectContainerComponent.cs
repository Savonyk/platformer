using UnityEngine;

namespace Scripts.Components.GameObjectsBased
{
    class GameObjectContainerComponent : MonoBehaviour
    {
        [SerializeField] 
        private GameObject[] _objects;
        [SerializeField]
        private DropItemEvent _onDropItems;

        public void Drop()
        {
            _onDropItems?.Invoke(_objects);
        }
    }
}
