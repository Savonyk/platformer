using UnityEngine;

namespace Scripts.Components.Effects
{
    class ParalaxEffect : MonoBehaviour
    {
        [SerializeField]
        private float _effectSpeed;
        [SerializeField]
        private Transform _followTarget;

        private float _startX;

        private void Start()
        {
            _startX = transform.position.x;
        }

        private void LateUpdate()
        {
            var currentPosition = transform.position;
            var deltaX = _followTarget.position.x * _effectSpeed;
            currentPosition.x = _startX + deltaX;
            transform.position = currentPosition;
        }
    }
}
