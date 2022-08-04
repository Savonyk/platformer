using UnityEngine;
using System.Collections;

namespace Scripts.Creatures.Mobs.AI.Coroutines
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}
