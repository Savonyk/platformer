using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


namespace Scripts.Components.GameObjectsBased
{
    public class DropByProbabilityComponent : MonoBehaviour
    {
        [SerializeField] 
        private int _count;
        [SerializeField] 
        private DropData[] _dropedItems;
        [SerializeField] 
        private DropItemEvent _onDropItems;
        [SerializeField] 
        private bool _spawnOnEnable;

        private void OnEnable()
        {
            if (_spawnOnEnable)
            {
                CalculateDrop();
            }
        }

        [ContextMenu("Drop")]
        public void CalculateDrop()
        {
            var itemsToDrop = new GameObject[_count];
            var itemCount = 0;
            var total = _dropedItems.Sum(dropData => dropData.Probability);
            var sortedDropItems = _dropedItems.OrderBy(dropData => dropData.Probability);

            while(itemCount < _count)
            {
                var random = Random.value * total;
                var currentProbability = 0.0f;
                foreach (var dropItem in sortedDropItems)
                {
                    currentProbability += dropItem.Probability;
                    if(currentProbability >= random)
                    {
                        itemsToDrop[itemCount] = dropItem.Item;
                        itemCount++;
                        break;
                    }
                }
            }

            _onDropItems?.Invoke(itemsToDrop);
        }

        [Serializable]
        public class DropData
        {
            [SerializeField] 
            private GameObject _item;
            [Range(0f, 100f)] [SerializeField] 
            private float _probability;

            public GameObject Item { get { return _item; } set { _item = value; } }
            public float Probability { get { return _probability; } set { _probability = value; } }
        }

        public void SetItemsCount(int count) => _count = count;
    }
    [Serializable]
    public class DropItemEvent : UnityEvent<GameObject[]> { }
}
