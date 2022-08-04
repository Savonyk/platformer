using UnityEngine;

namespace Scripts.Components.ColiderBased
{
    public class LayerCheckComponent : MonoBehaviour
    {
        [SerializeField] protected LayerMask _layer;
        [SerializeField] protected bool _isTouchingLayer;

        public bool IsTouching { get { return _isTouchingLayer; } }
    }
}
