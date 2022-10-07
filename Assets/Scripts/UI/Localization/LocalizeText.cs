using UnityEngine;
using UnityEngine.UI;
using Scripts.Model.Def.Localization;
using System;

namespace Scripts.UI.Localization
{
    [RequireComponent(typeof(Text)), Serializable]
    public class LocalizeText : MonoBehaviour
    {
        [SerializeField]
        private string _key;
        [SerializeField]
        private bool _capitalize;

        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            LocalizationManager.Instance.OnLocaleChanged += Localize;
            Localize();
        }

        private void Localize()
        {
            var text = LocalizationManager.Instance.Localize(_key);
            _text.text = _capitalize ? text.ToUpper() : text;
        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.OnLocaleChanged -= Localize;
        }
    }
}
