using UnityEngine;
using System.Linq;
using System;

namespace Scripts.Components.GameObjectsBased
{
   public class SpawnComponentList : MonoBehaviour
    {
        [SerializeField] 
        private SpawnData[] _spawners;

        public void SpawnAll()
        {
            foreach (var spawnData in _spawners)
            {
                spawnData.SpawningObject.Spawn();
            }
        }

        public void Spawn(string id)
        {
            var spawner = _spawners.FirstOrDefault(element => element.Id == id);
            spawner?.SpawningObject.Spawn();
        }

        [Serializable]
        public class SpawnData
        {
            public string Id;
            public SpawnComponent SpawningObject;
        }
    }
}
