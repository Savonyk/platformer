using UnityEngine;

namespace Scripts.Creatures.Weapon
{
    class BaseProjectile : MonoBehaviour
    {
        [SerializeField] private bool _invertX;
        [SerializeField] private float _speed;

        protected Rigidbody2D Rigidbody;
        private int _directionX;

        protected int DirectionX => _directionX;
        protected float Speed => _speed;

        protected virtual void Start()
        {
            var mode = _invertX ? -1 : 1;
            Rigidbody = GetComponent<Rigidbody2D>();
            _directionX = mode * transform.lossyScale.x > 0 ? 1 : -1;
        }
    }
}
