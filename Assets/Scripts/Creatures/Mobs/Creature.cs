using UnityEngine;
using Scripts.Components.ColiderBased;
using Scripts.Components.GameObjectsBased;
using Scripts.Components.Audio;

namespace Scripts.Creatures.Mobs
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] 
        private bool _isInvertScale;
        [SerializeField] 
        private float _speed;
        [SerializeField] 
        protected float JumpForce;
        [SerializeField] 
        private float _damageJumpForce;
        [SerializeField] 
        protected ColiderCheckComponent CheckGround;
        [SerializeField] 
        protected SpawnComponentList Particles;
        [SerializeField] 
        private CheckCircleOverlayComponent _attackRange;
        [SerializeField] 
        protected PlayClipComponent AudioClips;

        protected Animator Animator;
        protected Rigidbody2D Rigidbody;
        private Vector2 _direction;
        protected bool IsGrounded;
        private bool _isJumping;
        private static readonly int _isRunningKey = Animator.StringToHash("isRunning");
        private static readonly int _isGroundedKey = Animator.StringToHash("isGrounded");
        private static readonly int _verticalVelocityKey = Animator.StringToHash("verticalVelocity");
        private static readonly int _HitKey = Animator.StringToHash("hit");
        private static readonly int _AttackKey = Animator.StringToHash("attack");

        public float Speed { get => _speed; set => _speed = value;  }

        public Rigidbody2D GetRigidbody => Rigidbody;

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
        }

        public Vector2 Direction { get { return _direction; } set { _direction = value; } }

        protected virtual void Update()
        {
            IsGrounded = CheckGround.IsTouching;
        }

        private void FixedUpdate()
        {
            var xVelocity = _direction.x * CalculateSpeed();
            var yVelocity = CalculateYVelocity();

            Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            Animator.SetBool(_isRunningKey, Rigidbody.velocity.x != 0);
            Animator.SetBool(_isGroundedKey, IsGrounded);
            Animator.SetFloat(_verticalVelocityKey, Rigidbody.velocity.y);

            UpdateSrpiteDirection(_direction);
        }

        protected virtual float CalculateSpeed()
        {
            return _speed;
        }

        protected virtual float CalculateYVelocity()
        {
            var yVelocity = Rigidbody.velocity.y;

            bool isPressingJump = _direction.y > 0;
            if (IsGrounded)
            {
                _isJumping = false;
            }

            if (isPressingJump)
            {
                _isJumping = true;

                var isFalling = Rigidbody.velocity.y <= 0.001f;
                yVelocity = isFalling ? CalculateJumpVelocity(yVelocity) : yVelocity;

            }
            else if (Rigidbody.velocity.y > 0 && _isJumping)
            {

                yVelocity *= 0.5f;
            }
            return yVelocity;
        }

        protected void SpawnJumpVFX()
        {
            Particles.Spawn("Jump");
            AudioClips.Play("Jump");
        }

        protected virtual float CalculateJumpVelocity(float yVelocity)
        {
            if (IsGrounded)
            {

                yVelocity = JumpForce;
                SpawnJumpVFX();
            }
            return yVelocity;
        }

        public void UpdateSrpiteDirection(Vector2 direction)
        {
            var modifier = _isInvertScale ? -1 : 1;
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(modifier * 1, 1, 0);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(modifier  * - 1, 1, 0);
            }
        }

        public virtual void OnDamaged()
        {
            _isJumping = false;
            Animator.SetTrigger(_HitKey);
            AudioClips.Play("Hurt");
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageJumpForce);
        }

        public virtual void Attack()
        {
            Animator.SetTrigger(_AttackKey);

        }

        public virtual void OnDamageObject()
        {
           
            _attackRange.Check();
            Particles.Spawn("Attack");
            AudioClips.Play("Attack");
        }

    }
}
