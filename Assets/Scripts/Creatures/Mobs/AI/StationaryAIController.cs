using UnityEngine;
using Scripts.Components.ColiderBased;
using Scripts.Components.GameObjectsBased;
using Scripts.Components.LevelManagment;

namespace Scripts.Creatures.Mobs.AI
{
    public class StationaryAIController : MonoBehaviour
    {
        [SerializeField] 
        private ColiderCheckComponent _visionCheker;
        [Header("Melee Attack")] [SerializeField] 
        private CheckCircleOverlayComponent _melleAttack;
        [SerializeField] 
        private ColiderCheckComponent _meleeChecker;
        [SerializeField] 
        private ResetComponent _meleeCoolDown;
        [Header("Range Attack")] [SerializeField] 
        private SpawnComponent _rangeAttack;
        [SerializeField] 
        private ResetComponent _rangeCoolDown;

        private Animator _animator;
        private static readonly int _isMeleeAttackKey = Animator.StringToHash("melee");
        private static readonly int _isRangeAttackKey = Animator.StringToHash("range");

        private void Awake()
        {
            _animator = GetComponent<Animator>();

        }

        private void Update()
        {
            if (!_visionCheker.IsTouching) return;

            if (_meleeChecker.IsTouching)
            {
                if (_meleeCoolDown.IsReady)
                {
                    MeleeAttack();
                    return;
                }
            }
            if (_rangeCoolDown.IsReady)
            {
                RangeAttack();
            }
        }

        private void MeleeAttack()
        {
            _animator.SetTrigger(_isMeleeAttackKey);
            ResetTimers();
        }

        private void RangeAttack()
        {
            _animator.SetTrigger(_isRangeAttackKey);
            ResetTimers();
        }

        private void ResetTimers()
        {
            _meleeCoolDown.Reset();
            _rangeCoolDown.Reset();
        }

        public void OnMeleeAttack()
        {
            _melleAttack.Check();
        }

        public void OnRangeAttack()
        {
            _rangeAttack.Spawn();
        }
    }
}