using UnityEngine;

namespace Scripts.Components.Movement
{
    class VerticalLevitation : MonoBehaviour
    {
        [SerializeField] 
        private float _frequency = 1f;
        [SerializeField] 
        private float _amplitude = 1f;
        [SerializeField] 
        private bool _randomize;
        [SerializeField] 
        private bool _isMoving;

        private float _originalY;
        private Rigidbody2D _rigidbody;
        private float _seed;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _originalY = _rigidbody.position.y;
            _seed = _randomize ? Random.value * Mathf.PI * 2 : 0f;
        }

        private void Update()
        {
            if (!_isMoving) return;

            var position = _rigidbody.position;
            position.y = _originalY + Mathf.Sin(_seed + Time.time * _frequency) * _amplitude;
            _rigidbody.MovePosition(position);
        }
    }
}
