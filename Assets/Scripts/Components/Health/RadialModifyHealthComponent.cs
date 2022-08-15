using UnityEngine;
using UnityEditor;
using Scripts.Utils;

namespace Scripts.Components.Health
{
    class RadialModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int _radius;
        [SerializeField] private int _healthDelta;

        private Vector2 _targetPosition;
        private Vector2 _currentPosition;

        private Vector2 _maxPointForDamage;

        public void ChangeHealth(GameObject target)
        {

            var healthComponent = target.GetComponent<HealthComponent>();

            if (healthComponent == null) return;
 

            _targetPosition = target.transform.position;
            _currentPosition = transform.position;

            ChangeMaxPointToDamage();
            GetDistancesRelation();

            var distanceRelation = GetDistancesRelation();

            if (distanceRelation >= 1) return;

            var damage = (int)Mathf.Lerp(_healthDelta, 0, distanceRelation);

            healthComponent.ModifyHealth(damage);
        }

        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesColors.TransparentRed;
            Handles.DrawSolidDisc(transform.position, transform.forward, _radius);
        }

        private float GetDistancesRelation()
        {
            var maxDistance = (_maxPointForDamage - _currentPosition).magnitude;
            var currentDistance = (_targetPosition - _currentPosition).magnitude;

            var distanceRelation = currentDistance / maxDistance;
            return distanceRelation;
        }

        private void ChangeMaxPointToDamage()
        {
            var angle = GetAngle(_targetPosition, _currentPosition);
            _maxPointForDamage = GetUnitOnCircle(angle) * _radius + _currentPosition;

        }


        private float GetAngle(Vector2 currentPosition, Vector2 targetPosition)
        {
            var vector = currentPosition - targetPosition;
            var angle = Mathf.Atan2(vector.y, vector.x);
            return angle;
        }

        private Vector2 GetUnitOnCircle(float angleDegrees)
        {
            var x = Mathf.Cos(angleDegrees);
            var y = Mathf.Sin(angleDegrees);

            return new Vector2(x, y);
        }
    }
}
