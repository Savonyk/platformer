using UnityEngine;
using Scripts.Model.Data;
using Scripts.UI.Widgets;
using Scripts.Utils;

namespace Scripts.UI.Windows
{
    class SettingsWindow : AnimatedWindow
    {
        [SerializeField] 
        private AudioSettingsWidget _music;
        [SerializeField] 
        private AudioSettingsWidget _sfx;

        protected override void Start()
        {
            base.Start();

            _music.SetModel(GameSettings.Instance.Music);
            _sfx.SetModel(GameSettings.Instance.SFX);
        }

        public void ChangeLanguage()
        {
            OpenWindowUtils.CreateWindow("UI/LocalizationWindow");
        }
    }
}
