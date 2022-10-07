using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using Scripts.Model.Def.Localization;

namespace Scripts.UI.Widgets
{
    class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocaleInfo>
    {
        [SerializeField]
        private Text _text;
        [SerializeField]
        private GameObject _selector;
        [SerializeField]
        private SelectLocale _onSelected;

        private LocaleInfo _data;

        private void Start()
        {
            LocalizationManager.Instance.OnLocaleChanged += UpdateSelection;
        }

        public void SetData(LocaleInfo localeInfo, int index)
        {
            _data = localeInfo;
            UpdateSelection();
            _text.text = localeInfo.LocaleId.ToUpper();
        }

        private void UpdateSelection()
        {
            var isSelected = LocalizationManager.Instance.LocaleKey == _data.LocaleId;
            _selector.SetActive(isSelected);
        }

        public void OnSelected()
        {
            _onSelected?.Invoke(_data.LocaleId);
        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.OnLocaleChanged -= UpdateSelection;
        }
    }

    [Serializable]
    public class LocaleInfo {
        public string LocaleId;
    }

    [Serializable]
    public class SelectLocale : UnityEvent<string> { }
}
