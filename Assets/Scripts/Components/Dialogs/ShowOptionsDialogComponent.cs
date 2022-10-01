using UnityEngine;
using Scripts.UI.HUD.Dialogs;

namespace Scripts.Components.Dialogs
{
    class ShowOptionsDialogComponent : MonoBehaviour
    {
        [SerializeField]
        private OptionDialogData _dialogData;

        private OptionalDialogController _controller;

        public void Show()
        {
            if(_controller == null)
            {
                _controller = FindObjectOfType<OptionalDialogController>();
            }

            _controller.ShowDialog(_dialogData);
        }
    }
}
