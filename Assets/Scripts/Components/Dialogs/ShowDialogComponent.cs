using System;
using UnityEngine;
using Scripts.Model.Data;
using Scripts.Model.Def;
using Scripts.UI.HUD.Dialogs;

namespace Scripts.Components.Dialogs
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField]
        private Mode _mode;
        [SerializeField]
        private DialogData _bounded;
        [SerializeField]
        private DialogDefinition _external;

        private DialogBoxController _dialogBox;

        public DialogData Data
        {
            get {
                switch (_mode)
                {
                    case Mode.Bound:
                        return _bounded;
                    case Mode.External:
                        return _external.Data;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void Show()
        {
            if(_dialogBox == null)
            {
                _dialogBox = FindObjectOfType<DialogBoxController>();
                Debug.LogError("AAAA");
            }

            _dialogBox.ShowDialog(Data);
        }

        public void Show(DialogDefinition definition)
        {
            _external = definition;
            Show();
        }

        [Serializable]
        public enum Mode
        {
            Bound,
            External
        }
    }
}
