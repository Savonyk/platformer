using UnityEngine;
using UnityEngine.Events;
using System;
using System.Linq;

namespace Scripts.Components.ColiderBased
{
    public abstract class CheckFigureOverlayComponent : MonoBehaviour
    {
        [SerializeField] protected LayerMask _mask;
        [SerializeField] protected string[] _tags;
        [SerializeField] protected OnCheckOverlap _onCheckOverlap;

        protected readonly Collider2D[] _results = new Collider2D[5];

        protected abstract void OnDrawGizmosSelected();

        protected abstract int GetSize();

        public void Check()
        {
            var size = GetSize();

            for (int i = 0; i < size; i++)
            {
                var overlapingResult = _results[i];
                var isInTags = _tags.Any(tag => overlapingResult.CompareTag(tag));
                if (isInTags)
                {
                    _onCheckOverlap?.Invoke(overlapingResult.gameObject);
                }
            }
        }



        [Serializable]
      public class OnCheckOverlap : UnityEvent<GameObject> { }
    }
}
