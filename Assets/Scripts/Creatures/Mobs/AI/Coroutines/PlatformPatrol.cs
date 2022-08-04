using System.Collections;
using UnityEngine;
using Scripts.Components.ColiderBased;


namespace Scripts.Creatures.Mobs.AI.Coroutines
{
    public class PlatformPatrol : Patrol
    {
        [SerializeField] private LineCastCheckComponent _checkGround;
        [SerializeField] private LineCastCheckComponent _checkObstacles;

        private Creature _AIPlayer;

        private int _invertXvalue = 1;

        private void Awake()
        {
            _AIPlayer = GetComponent<Creature>();
        }

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (!_checkGround.IsTouching || _checkObstacles.IsTouching)
                {
                    _invertXvalue = -_invertXvalue;  
                }

                _AIPlayer.Direction = new Vector2(-1 * _invertXvalue, 0);

                yield return null;
            }
        }
    }
}
