using UnityEngine;
using System.Collections;
using UnityEditor;
using Random = UnityEngine.Random;
using Scripts.Utils;

namespace Scripts.Components.GameObjectsBased
{
    public class RandomSpawnerComponent : MonoBehaviour
    {
        [Header("Spawn bound:")] [SerializeField] 
        private float _sectorAngle = 60;
        [SerializeField] 
        private float _sectorRotation;
        [SerializeField] 
        private float _waitTime = 0.1f;
        [SerializeField] 
        private float _speed = 6f;

        private Coroutine _routine;

        public void StartDrop(GameObject[] items)
        {
            TryToStopRoutine();

            _routine = StartCoroutine(StartSpawn(items));
        }

        private void TryToStopRoutine()
        {
            if (_routine != null)
            {

                StopCoroutine(_routine);
            }
        }

        private IEnumerator StartSpawn(GameObject[] particles)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                Spawn(particles[i]);

                yield return new WaitForSeconds(_waitTime);
            }
        }

        public void DropImmediate(GameObject[] items)
        {
            foreach (var item in items)
            {
                Spawn(item);
            }
        }

        private void Spawn(GameObject particle)
        {
            var instance = SpawnUtils.Spawn(particle, transform.position, Quaternion.identity);
            var rigidbody = instance.GetComponent<Rigidbody2D>();

            var randomAngle = Random.Range(0, _sectorAngle);
            var forceVector = AngleToVectorInSector(randomAngle);
            rigidbody.AddForce(forceVector * _speed, ForceMode2D.Impulse);
        }

        private void OnDrawGizmosSelected()
        {
            var position = transform.position;

            var middleAngleDeelta = (180 - _sectorRotation - _sectorAngle) / 2;
            var rightBound = GetUnitOnCircle(middleAngleDeelta);
            Handles.DrawLine(position, position + rightBound);

            var leftBound = GetUnitOnCircle(middleAngleDeelta + _sectorAngle);
            Handles.DrawLine(position, position + leftBound);
            Handles.DrawWireArc(position, Vector3.forward, rightBound, _sectorAngle, leftBound.magnitude);

            Handles.color = new Color(1f, 1f, 1f, 0.1f);
            Handles.DrawSolidArc(position, Vector3.forward, rightBound,_sectorAngle, leftBound.magnitude);
        }

        private Vector2 AngleToVectorInSector(float angle)
        {
            var angleMiddleDelta = (180 - _sectorRotation - _sectorAngle) / 2;
            return GetUnitOnCircle(angle + angleMiddleDelta);
        }

        private Vector3 GetUnitOnCircle(float angleDegrees)
        {
            var angleRadians = angleDegrees * Mathf.PI / 180.0f;

            var x = Mathf.Cos(angleRadians);
            var y = Mathf.Sin(angleRadians);

            return new Vector3(x, y, 0);
        }

        private void OnDisable()
        {
            TryToStopRoutine();
        }

        private void OnDestroy()
        {
            TryToStopRoutine();
        }
    }
}
