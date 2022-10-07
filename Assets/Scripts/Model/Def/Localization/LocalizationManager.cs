using System;
using System.Collections.Generic;
using Scripts.Model.Data.Properties;
using UnityEngine;

namespace Scripts.Model.Def.Localization
{
    public class LocalizationManager
    {
        public static readonly LocalizationManager Instance;

        private readonly StringPersistentPropert _localeKey = new ("en", "Localization/current");

        private Dictionary<string, string> _localization;

        public string LocaleKey => _localeKey.Value;

        public event Action OnLocaleChanged;

        static LocalizationManager()
        {
            Instance = new LocalizationManager();
        }

        public LocalizationManager()
        {
            LoadLocale(_localeKey.Value);
        }

        private void LoadLocale(string pathToLoad)
        {
            var localeDefinition = Resources.Load<LocaleDefinition>($"Locales/{pathToLoad}");
            _localization = localeDefinition.GetData();
            _localeKey.Value = pathToLoad;
            OnLocaleChanged?.Invoke();
        }

        public string Localize(string key)
        {
            return _localization.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";
        }

        public void SetLocale(string localeKey)
        {
            LoadLocale(localeKey);
        }
    }
}
