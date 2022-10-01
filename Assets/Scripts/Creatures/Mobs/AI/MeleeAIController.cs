using UnityEngine;
using System.Collections;
using Scripts.Creatures.Mobs.AI.Coroutines;
using Scripts.Components.GameObjectsBased;

namespace Scripts.Creatures.Mobs.AI 
{
    public class MeleeAIController : BaseAIController
    {

        [SerializeField] 
        private SpawnComponentList _particles;
        [SerializeField] 
        private float _alarmDelay = 1.5f;
        [SerializeField] 
        private float _missingTime = 1.5f;
        [SerializeField]
        private float _thresholdByX = 0.2f;

        private Patrol _patrol;

        protected override void Awake()
        {
            base.Awake();
            _particles = GetComponent<SpawnComponentList>();
            _patrol = GetComponent<Patrol>();
        }

        private void Start()
        {
            StartState(_patrol.DoPatrol());
        }
        

        public override void OnHeroVision(GameObject target)
        {
            if (IsDead) return;
            Target = target;
            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(_alarmDelay);
            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            AIPlayer.UpdateSrpiteDirection(direction);
        }

        private IEnumerator GoToHero()
        {
            while (Vision.IsTouching)
            {
                if (CanAttack.IsTouching)
                {
                    StartState(AttackHero());
                }
                else
                {
                    var directionX = Target.transform.position.x - transform.position.x;
                    var directionToTarget = directionX <= _thresholdByX ? Vector2.zero : GetDirectionToTarget();
                    SetDirectionToTarget(directionToTarget);
     
                }
                yield return null;
            }
            _particles.Spawn("Miss");
            yield return new WaitForSeconds(_missingTime);

            StartState(_patrol.DoPatrol());
        }

        protected override IEnumerator AttackHero()
        {
            while (CanAttack.IsTouching)
            {
                AIPlayer.Attack();
                yield return new WaitForSeconds(AttackCoolDown);
            }

            StartState(GoToHero());
        }

    }
}