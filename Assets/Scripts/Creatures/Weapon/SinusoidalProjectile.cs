using UnityEngine;

namespace Scripts.Creatures.Weapon
{
    class SinusoidalProjectile : BaseProjectile
    {
        [SerializeField] 
        private float _amplitude;
        [SerializeField] 
        private float _frequency;

        private float _originalY;
        private float _time;

        protected override void Start()
        {
            base.Start();
            _originalY = Rigidbody.position.y;
            _time = 0f;
        }

        private void FixedUpdate()
        {
            var position = Rigidbody.position;
            position.x += DirectionX * Speed;
            position.y = _originalY + Mathf.Sin(_time * _frequency) * _amplitude;
            Rigidbody.MovePosition(position);
            _time += Time.fixedDeltaTime;
        }
    }
}
