using UnityEngine;
using Scripts.Utils;
using System.Collections.Generic;

namespace Scripts.Components.Movement
{
    class CircleLevitation : MonoBehaviour
    {
        [SerializeField] 
        private float _radius;
        [SerializeField] 
        private float _speed;

        private Rigidbody2D[] _coins;
        private Vector2[] _positions;

        private float _time;

        private void Awake()
        {
            UpdateContent();
        }

        private void UpdateContent()
        {
            _coins = GetComponentsInChildren<Rigidbody2D>();
            _positions = new Vector2[_coins.Length];
        }

        private void FixedUpdate()
        {
            CalculatePositions();
            var isAllDestroyed = true;
            for (int i = 0; i < _coins.Length; i++)
            {
                if (_coins[i])
                {
                    _coins[i].MovePosition(_positions[i]);
                    isAllDestroyed = false;
                }
            }

            if (isAllDestroyed)
            {
                enabled = false;
                Destroy(gameObject, 1f);
            }

            _time += Time.fixedDeltaTime;
        }

        private void CalculatePositions()
        {
            var step = 2 * Mathf.PI / _coins.Length;
            Vector2 containerPosition = transform.position;

            for (int i = 0; i < _coins.Length; i++)
            {
                var angle = step * i;
                var xPosition = Mathf.Cos(angle + _time * _speed) * _radius;
                var yPosition = Mathf.Sin(angle + _time * _speed) * _radius;
                var position = new Vector2(xPosition, yPosition);

                _positions[i] = containerPosition + position;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UpdateContent();
            CalculatePositions();
            for (int i = 0; i < _coins.Length; i++)
            {
                _coins[i].transform.position = _positions[i];
            }
        }

        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, _radius);
        }
#endif

    }
}
