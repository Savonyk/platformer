using UnityEngine;
using UnityEditor;
using Scripts.Utils;
using System.Linq;
using System;
using UnityEngine.Events;


namespace Scripts.Components.ColiderBased
{
   public class CheckCircleOverlayComponent : MonoBehaviour
    {
        [SerializeField] private float _radius;

        [SerializeField] protected LayerMask _mask;
        [SerializeField] protected string[] _tags;
        [SerializeField] protected OnCheckOverlap _onCheckOverlap;

        private readonly Collider2D[] _results = new Collider2D[5];

        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesColors.TransparentRed;
            Handles.DrawSolidDisc(transform.position, transform.forward, _radius);
        }

        private int GetSize()
        {
            return Physics2D.OverlapCircleNonAlloc(transform.position,//
                _radius,//
                _results//
                , _mask
                        );
        }


        public void Check()
        {
            var size = GetSize();

            for (int i = 0; i < size; i++)
            {
                var overlapingResult = _results[i];
                var isInTags = _tags.Any(tag => overlapingResult.CompareTag(tag));
                if (isInTags)
                {
                    Debug.LogError("Fuck");
                    _onCheckOverlap?.Invoke(overlapingResult.gameObject);
                }
            }
        }



        [Serializable]
        public class OnCheckOverlap : UnityEvent<GameObject> { }
    }
}
