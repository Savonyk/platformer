﻿using UnityEngine;


namespace Scripts.UI.Windows
{
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int ShowKey = Animator.StringToHash("show");
        private static readonly int HideKey = Animator.StringToHash("hide");

        protected virtual void Start()
        {
            _animator = GetComponent<Animator>();

            _animator.SetTrigger(ShowKey);
        }

        public void Close()
        {
            _animator.SetTrigger(HideKey);
        }

        public virtual void OnCloseAnimationCompleted()
        {
            Destroy(gameObject);
        }
    }
}
