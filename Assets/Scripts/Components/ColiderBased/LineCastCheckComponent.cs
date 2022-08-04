using UnityEngine;
using Scripts.Utils;

namespace Scripts.Components.ColiderBased
{
    public class LineCastCheckComponent : LayerCheckComponent
    {
        [SerializeField] private Transform _target;

        private readonly RaycastHit2D[] _result = new RaycastHit2D[1];

        private void Update()
        {
            _isTouchingLayer = Physics2D.LinecastNonAlloc(transform.position, _target.position, _result, _layer) > 0;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.color = HandlesColors.Green;
            UnityEditor.Handles.DrawLine(transform.position, _target.position);
        }
#endif
    }
}
