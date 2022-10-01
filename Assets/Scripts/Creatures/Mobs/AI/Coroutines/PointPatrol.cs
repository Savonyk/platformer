using System.Collections;
using UnityEngine;

namespace Scripts.Creatures.Mobs.AI.Coroutines
{
    public class PointPatrol : Patrol
    {
        [SerializeField] 
        private Transform[] _points;
        [SerializeField] 
        private float _destinationTreshold = 1f;

        private int _destinationPointIndex;
        private Creature _AIPlayer;

        private void Awake()
        {
            _AIPlayer = GetComponent<Creature>();
        }

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (IsOnPoint())
                {
                    _destinationPointIndex = (int)Mathf.Repeat(_destinationPointIndex + 1, _points.Length);
                }

                var direction = _points[_destinationPointIndex].position - transform.position;
                direction.y = 0;
                _AIPlayer.Direction = direction.normalized;

                yield return null;
            }
        }

        private bool IsOnPoint()
        {
            return (_points[_destinationPointIndex].position - transform.position).magnitude <=
                _destinationTreshold;
        }
    }
}
