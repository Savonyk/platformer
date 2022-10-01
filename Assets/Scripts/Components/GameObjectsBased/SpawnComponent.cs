using UnityEngine;
using Scripts.Utils;
using System;

namespace Scripts.Components.GameObjectsBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] 
        private Transform _target;
        [SerializeField] 
        private GameObject _prefabToSpawn;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instantiate = SpawnUtils.Spawn(_prefabToSpawn, _target.position, Quaternion.identity);
            instantiate.transform.localScale = _target.lossyScale;
            instantiate.SetActive(true);
        }

        public void SetPrefab(GameObject prefab)
        {
            _prefabToSpawn = prefab;
        }
    }
}