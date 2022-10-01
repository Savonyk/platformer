using System;
using UnityEngine;
using UnityEngine.Events;
using Scripts.Utils;

namespace Scripts.Components.ColiderBased
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] 
        private LayerMask _layer;
        [SerializeField] 
        private string _tag = "Player";
        [SerializeField] 
        private EnterEvent _action;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.IsInLayer(_layer)) return;

            if (!string.IsNullOrEmpty(collision.gameObject.tag) && !collision.gameObject.CompareTag(_tag)) return;

            _action?.Invoke(collision.gameObject);
        }

        [Serializable]
        public class EnterEvent : UnityEvent<GameObject> { }

    }
}