using UnityEngine;
using Scripts.Model.Data;
using Scripts.UI.Widgets;

namespace Scripts.UI.Settings
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
    }
}
