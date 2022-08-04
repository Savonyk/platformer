using UnityEngine;
using System.Collections;
using Scripts.Components.ColiderBased;

namespace Scripts.Creatures.Mobs.AI
{
    public abstract class BaseAIController : MonoBehaviour
    {
        [SerializeField] protected ColiderCheckComponent Vision;
        [SerializeField] protected ColiderCheckComponent CanAttack;
        [SerializeField] protected float AttackCoolDown = 2;

        protected IEnumerator CurrentCoroutine;
        private static readonly int _isDeadKey = Animator.StringToHash("isDead");

        protected Creature AIPlayer;
        protected bool IsDead;
        protected GameObject Target;
        protected Animator Animator;

        protected virtual void Awake()
        {
            AIPlayer = GetComponent<Creature>();
            Animator = GetComponent<Animator>();
        }

        public abstract void OnHeroVision(GameObject target);
        protected abstract IEnumerator AttackHero();

        protected void StartState(IEnumerator coroutine)
        {
            StopCurrentCoroutine();

            CurrentCoroutine = coroutine;
            StartCoroutine(CurrentCoroutine);
        }

        protected void StopCurrentCoroutine()
        {
            AIPlayer.Direction = Vector2.zero;
            if (CurrentCoroutine != null)
            {
                StopCoroutine(CurrentCoroutine);
            }
        }

        public virtual void OnDie()
        {

            IsDead = true;
            Animator.SetBool(_isDeadKey, IsDead);
            StopCurrentCoroutine();
        }

        protected Vector2 GetDirectionToTarget()
        {
            var direction = Target.transform.position - transform.position;
            direction.y = 0f;
            return direction.normalized;
        }

        protected void SetDirectionToTarget()
        {
            AIPlayer.Direction = GetDirectionToTarget();
        }
    }
}
