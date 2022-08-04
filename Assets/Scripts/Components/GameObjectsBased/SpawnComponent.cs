using UnityEngine;

namespace Scripts.Components.GameObjectsBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefabToSpawn;

        public Transform Target { get { return _target; } set { _target = value; } }
        public GameObject PrefabToSpawn { get { return _prefabToSpawn; } set { _prefabToSpawn = value; } }

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instantiate = Instantiate(_prefabToSpawn, _target.position, Quaternion.identity);
            instantiate.transform.localScale = _target.lossyScale;
            instantiate.SetActive(true);
        }
    }
}