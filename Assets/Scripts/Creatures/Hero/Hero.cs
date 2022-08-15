using Scripts.Utils;
using Scripts.Model;
using UnityEditor.Animations;
using UnityEngine;
using Scripts.Components.ColiderBased;
using Scripts.Components.GameObjectsBased;
using Scripts.Creatures.Mobs;
using Scripts.Components.LevelManagment;
using Scripts.Components.Health;
using System.Collections;
using Scripts.Model.Data;
using UnityEngine.SceneManagement;
using Cinemachine;

namespace Scripts.Creatures.Hero
{

    public class Hero : Creature, ICanAddInInventory
    {
        [Header("Checkers")]
        [SerializeField] private ColiderCheckComponent _wallChecker;

        [SerializeField] private CheckCircleOverlayComponent _interactionChecker;

        [Header("Throwing")]
        [SerializeField] private ResetComponent _throwCoolDown;
        [SerializeField] private ResetComponent _superTrowCoolDown;
        [SerializeField] private int _swordsThrowCount;
        [SerializeField] private float _superThrowDelay;


        [Header("Particles")]
        [SerializeField] private DropByProbabilityComponent _hitDrop;

        [Header("Animators")]
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _unarmed;

        private float _defaultGravityForce;
        private bool _isAllowingDoubleJump;
        private bool _isRepluseToWall;
        private bool _isSuperThrow;

        private GameSession _currentSession;
        private HealthComponent _healthComponent;

        private readonly static int _ThrowKey = Animator.StringToHash("throw");
        private readonly static int _IsOnWallKey = Animator.StringToHash("isOnWall");

        private int CoinsCount => _currentSession.Data.Inventory.ItemCount("Coin");
        private int SwordsCount => _currentSession.Data.Inventory.ItemCount("Sword");

        protected override void Awake()
        {
            base.Awake();
            _defaultGravityForce = Rigidbody.gravityScale;
            _isSuperThrow = false;
        }

        private void Start()
        {
            _currentSession = FindObjectOfType<GameSession>();

            _healthComponent = GetComponent<HealthComponent>();
            _healthComponent.Health = _currentSession.Data.Health.Value;
            _currentSession.Data.Inventory.OnChanged += OnInventoryChanged;
            UpdateHeroWeapon();
        }

        protected override void Update()
        {
            base.Update();

            var isMoveToWall = Direction.x * transform.lossyScale.x > 0f;

            if (IsOnWall() && isMoveToWall)
            {
                _isRepluseToWall = true;
                Rigidbody.gravityScale = 0f;
            }
            else
            {
                _isRepluseToWall = false;
                Rigidbody.gravityScale = _defaultGravityForce;
            }
            Animator.SetBool(_IsOnWallKey, _isRepluseToWall);
        }

        private void OnInventoryChanged(string id, int value)
        {
            if(id == "Sword")
            {
                UpdateHeroWeapon();
            }
        }

        private void OnDestroy()
        {
            _currentSession.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        protected override float CalculateYVelocity()
        {

            bool isPressingJump = Direction.y > 0;

            if (IsGrounded || _isRepluseToWall)
            {
                _isAllowingDoubleJump = true;
            }

            if (!isPressingJump && _isRepluseToWall)
            {
                return 0f;

            }
           
            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelocity)
        {

            if (!IsGrounded && _isAllowingDoubleJump && !_isRepluseToWall)
            {
                SpawnJumpVFX();
                _isAllowingDoubleJump = false;
                return JumpForce;
            }

            return base.CalculateJumpVelocity(yVelocity);
        }

       

        private bool IsOnWall()
        {
            return _wallChecker.IsTouching;
        }

        public void AddInInventory(string id, int value)
        {
            _currentSession.Data.Inventory.Add(id, value);
        }

        public override void OnDamaged()
        {
            base.OnDamaged();
            if(CoinsCount > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numberCoinsToDispose = Mathf.Min(CoinsCount, 5);
            _currentSession.Data.Inventory.Remove("Coin", numberCoinsToDispose);

            _hitDrop.SetItemsCount(numberCoinsToDispose);
            _hitDrop.CalculateDrop(); 
        }

        public void Interact()
        {
            _interactionChecker.Check();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.IsInLayer(CheckGround.Layer))
            {
                if (collision.contactCount != 0)
                {
                    var contact = collision.GetContact(0);

                    if (contact.relativeVelocity.y >= JumpForce)
                    {
                        Particles.Spawn("SlamDown");
                    }
                }
            }
        }

        public override void Attack()
        {
            if (SwordsCount <= 0) return;

            base.Attack();

        }

        public void StartTrowing()
        {
            _superTrowCoolDown.Reset();
        }

        public void PerformTrowing()
        {
            if (SwordsCount <= 1 || !_throwCoolDown.IsReady) return;

            if (_superTrowCoolDown.IsReady)
            {
                _isSuperThrow = true;
            }

            Animator.SetTrigger(_ThrowKey);
            AudioClips.Play("Throw");
            _throwCoolDown.Reset();
        }

        public void OnThrowSword()
        {
            if (_isSuperThrow)
            {
                var numThrows = Mathf.Min(_swordsThrowCount, SwordsCount - 1);
                StartCoroutine(Co_SuperThrow(numThrows));
            }
            else
            {
                ThrowAndRemoveFromInventory();
            }
            _isSuperThrow = false;
        }

        private IEnumerator Co_SuperThrow(int numThrows)
        {
            for (int i = 0; i < numThrows; i++)
            {
                ThrowAndRemoveFromInventory();
                yield return new WaitForSeconds(_superThrowDelay);
            }
        }

        private void ThrowAndRemoveFromInventory()
        {
            Particles.Spawn("Throw");
            _currentSession.Data.Inventory.Remove("Sword", 1);
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordsCount > 0 ? _armed : _unarmed;
        }

        public void UseCurrentItem()
        {
            var healthPotion = _currentSession.Data.Inventory.ItemCount("HealthPotion");
            if (healthPotion <= 0) return;

            _healthComponent.ModifyHealth(5);
            _currentSession.Data.Inventory.Remove("HealthPotion", 1);
        }

        public void ChangeHealth(int health)
        {
            _currentSession.Data.Health.Value = health;
        }
    }
}