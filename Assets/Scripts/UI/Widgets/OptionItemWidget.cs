using Scripts.UI.HUD.Dialogs;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Scripts.UI.Widgets
{
    class OptionItemWidget : MonoBehaviour, IItemRenderer<OptionData>
    {
        [SerializeField]
        private Text _label;
        [SerializeField]
        private OnItemSelected _onSelected;

        private OptionData _data;

        public void SetData(OptionData data, int index)
        {
            _data = data;
            _label.text = data.Text;
        }

        public void OnSelect()
        {
            _onSelected.Invoke(_data);
        }
    }

    [System.Serializable]
    public class OnItemSelected : UnityEvent<OptionData> { }
}
