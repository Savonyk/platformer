using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Widgets
{
    public class CustomButton : Button
    {
        [SerializeField] 
        private GameObject _normal;
        [SerializeField] 
        private GameObject _pressed;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state, instant);

            var isPressed = state == SelectionState.Pressed;

            _normal.SetActive(!isPressed);
            _pressed.SetActive(isPressed);
        }
    }
}
