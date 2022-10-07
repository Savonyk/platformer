using UnityEngine;
using Scripts.UI.Widgets;
using System.Collections.Generic;
using Scripts.Model.Def.Localization;
using Scripts.Utils;

namespace Scripts.UI.Windows
{
    class LocalizationWindow : AnimatedWindow
    {
        [SerializeField]
        private LocaleItemWidget _prefab;
        [SerializeField]
        private Transform _container;

        private DataGroup<LocaleInfo, LocaleItemWidget> _dataGroup;

        private string[] _supportedLocales = { "en", "ua", "ru" };

        protected override void Start()
        {
            base.Start();

            _dataGroup = new DataGroup<LocaleInfo, LocaleItemWidget>(_prefab, _container);
            _dataGroup.SetData(ComposeData());
        }

        private List<LocaleInfo> ComposeData()
        {
            var data = new List<LocaleInfo>();

            foreach (var locale in _supportedLocales)
            {
                data.Add(new LocaleInfo { LocaleId = locale });
            }

            return data;
        }

        public void OnSelected(string selectedLocale)
        {
            LocalizationManager.Instance.SetLocale(selectedLocale);
        }
    }
}
