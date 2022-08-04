﻿using UnityEngine;

namespace Scripts.Components.Interactions
{
    class SwitchComponent: MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private bool _state;
        [SerializeField] private string _animationKey;


        public void Switch()
        {
            _state = !_state;
            _animator.SetBool(_animationKey, _state);
        }

        [ContextMenu("SwitchInit")]
        public void SwitchInit()
        {
            Switch();
        }
    }
}