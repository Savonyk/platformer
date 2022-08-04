using UnityEngine;
using UnityEditor;
using Scripts.Utils;
using System.Linq;


namespace Scripts.Components.ColiderBased
{
   public class CheckCircleOverlayComponent : CheckFigureOverlayComponent
    {
        [SerializeField] private float _radius;

        protected override void OnDrawGizmosSelected()
        {
            Handles.color = HandlesColors.TransparentRed;
            Handles.DrawSolidDisc(transform.position, transform.forward, _radius);
        }

        protected override int GetSize()
        {
            return Physics2D.OverlapCircleNonAlloc(transform.position,//
                _radius,//
                _results//
                        //, _mask
                        );
        }
    }
}
