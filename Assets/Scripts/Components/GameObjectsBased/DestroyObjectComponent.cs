using UnityEngine;

namespace Scripts.Components.GameObjectsBased
{

    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _objectToDestroy;

        public void OnDestroyObject()
        {
            Destroy(_objectToDestroy);
        }
    }
}