using UnityEngine;
using System.Collections;
using Scripts.Creatures.Mobs.AI.Coroutines;
using Scripts.Components.GameObjectsBased;
using Scripts.Components.Health;

namespace Scripts.Creatures.Mobs.AI
{
    public class ExplodingAIController : BaseAIController
    {
        [SerializeField] 
        private float _missingTime = 1.5f;
        [SerializeField] 
        private SpawnComponent _explosion;
        [SerializeField] 
        private float _attackSpeed;

        private float _defaultSpeed;
        private Patrol _patrol;

        protected override void Awake()
        {
            base.Awake();
            _patrol = GetComponent<Patrol>();
            _defaultSpeed = AIPlayer.Speed;
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }

        public override void OnHeroVision(GameObject target)
        {
            if (IsDead) return;
            Target = target;
            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            AIPlayer.UpdateSrpiteDirection(direction);
        }

        private IEnumerator GoToHero()
        {
            LookAtHero();
            while (Vision.IsTouching)
            {
                if (CanAttack.IsTouching)
                {
                    StartState(AttackHero());
                }
                else
                {
                    AIPlayer.Speed = _attackSpeed;
                    SetDirectionToTarget(GetDirectionToTarget());
                }
                yield return null;
            }
            yield return new WaitForSeconds(_missingTime);

            AIPlayer.Speed = _defaultSpeed;
            StartState(_patrol.DoPatrol());
        }

        public override void OnDie()
        {
            _explosion.Spawn();

        }

        protected override IEnumerator AttackHero()
        {
            var healthComponent = AIPlayer.GetComponent<HealthComponent>();

            if (healthComponent != null)
            {
                healthComponent.ModifyHealth(-healthComponent.Health);
            }

            yield return null;
            StopCurrentCoroutine();
        }
    }
}
