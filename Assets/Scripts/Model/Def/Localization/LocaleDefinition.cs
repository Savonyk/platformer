using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace Scripts.Model.Def.Localization
{
    [CreateAssetMenu(menuName = "Definition/Locale", fileName = "Locale")]
    class LocaleDefinition : ScriptableObject
    {
        [SerializeField]
        private string _url;
        [SerializeField]
        private List<LocaleItem> _localeItems;

        private UnityWebRequest _request;

        [ContextMenu("Update locale")]
        public void UpdateLocale()
        {
            if (_request != null) return;

            _request = UnityWebRequest.Get(_url);
            _request.SendWebRequest().completed += OnDataLoaded;
        }

        public Dictionary<string, string> GetData()
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var locale in _localeItems)
            {
                dictionary.Add(locale.Key, locale.Value);
            }

            return dictionary;
        }

        private void OnDataLoaded(AsyncOperation operation)
        {
            if (operation.isDone)
            {
                _localeItems.Clear();
                var rows = _request.downloadHandler.text.Split('\n');
                foreach (var row in rows)
                {
                    AddLocalItem(row);
                }
            }
        }

        private void AddLocalItem(string row)
        {
            try
            {
                var parametres = row.Split('\t');
                _localeItems.Add(new LocaleItem { Key = parametres[0], Value = parametres[1] });
            }
            catch (Exception e)
            {
                Debug.LogError($"Can't parse row: {row}.\n Exception: {e}");               
            }
        }

        private void OnDestroy()
        {
            _request.SendWebRequest().completed -= OnDataLoaded;
        }
    }

    [Serializable]
    public class LocaleItem
    {
        public string Key;
        public string Value;
    }
}
