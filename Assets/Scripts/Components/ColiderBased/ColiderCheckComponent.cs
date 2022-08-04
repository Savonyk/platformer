using UnityEngine;

namespace Scripts.Components.ColiderBased
{

    public class ColiderCheckComponent : LayerCheckComponent
    {
        public LayerMask Layer { get { return _layer; } }


        private Collider2D _colider;

        private void Awake()
        {
            _colider = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            _isTouchingLayer = _colider.IsTouchingLayers(Layer);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _isTouchingLayer = _colider.IsTouchingLayers(Layer);
        }
    }
}