using UnityEngine.InputSystem;
using UnityEngine;
using Scripts.Utils;

namespace Scripts.Creatures.Hero
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] 
        private Hero _character;

        public void OnMovevemnt(InputAction.CallbackContext context)
        {

            _character.Direction = context.ReadValue<Vector2>();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _character.Interact();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _character.Attack();
            }
        }

        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.started) {
                _character.StartTrowing();
            }
            else if (context.canceled)
            {
                _character.PerformTrowing();
            }

        }

        public void OnUsedItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _character.UseCurrentItem();
            }
        }

        public void OnNextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _character.NextItem();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _character.OnPerformedPause();
            }
        }
    }
}