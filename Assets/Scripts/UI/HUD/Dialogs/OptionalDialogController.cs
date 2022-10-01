using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Scripts.UI.Widgets;

namespace Scripts.UI.HUD.Dialogs
{
    class OptionalDialogController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _container;
        [SerializeField]
        private Text _textContainer;
        [SerializeField]
        private OptionItemWidget _prefab;

        private DataGroup<OptionData, OptionItemWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<OptionData, OptionItemWidget>(_prefab, _container.transform);
        }

        public void OnOptionsSelected(OptionData selectedOptions)
        {
            selectedOptions.OnSelected.Invoke();
            _container.SetActive(false);
        }

        public void ShowDialog(OptionDialogData data)
        {
            _container.SetActive(true);
            _textContainer.text = data.DialogText;
            _dataGroup.SetData(data.Options);
        }

    }

    [Serializable]
    public class OptionDialogData
    {
        public string DialogText;
        public OptionData[] Options;
    }

    [Serializable]
    public class OptionData
    {
        public string Text;
        public UnityEvent OnSelected;
    }
}
