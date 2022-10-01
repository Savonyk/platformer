using UnityEngine;
using System.Collections.Generic;

namespace Scripts.Components.CutScenes
{
    class ShowTargetComponent : MonoBehaviour
    {
        [SerializeField] 
        private Transform _target;
        [SerializeField] 
        private CameraStateController _controller;
        [SerializeField] 
        private float _delay = 2f;


        private void OnValidate()
        {
            if (_controller == null)
            {
                _controller = FindObjectOfType<CameraStateController>();
            }
        }


        public void ShowTarget()
        {

            _controller.SetPosition(_target.position);
            _controller.SetState(true);
            Invoke(nameof(MoveBack), _delay);
        }

        public void MoveBack()
        {
            _controller.SetState(false);
        }
    }
}
