using System;
using Scripts.Model.Data;
using Scripts.Model.Data.Properties;
using UnityEngine;

namespace Scripts.Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettingsComponent : MonoBehaviour
    {
        [SerializeField] private SoundSettings _mode;

        private FloatPersistentProperty _model;
        private AudioSource _source;


        private void Start()
        {
            _source = GetComponent<AudioSource>();

            _model = FindProperty();
            _model.OnChanged += OnSoundSettingChanged;
            OnSoundSettingChanged(_model.Value, _model.Value);
        }

        private void OnSoundSettingChanged(float newValue, float oldValue)
        {
            _source.volume = newValue;
        }

        private FloatPersistentProperty FindProperty()
        {
            switch (_mode)
            {
                case SoundSettings.Music:
                    return GameSettings.Instance.Music;
                case SoundSettings.SFX:
                    return GameSettings.Instance.SFX;
                default:
                    throw new ArgumentException("Undefined mode");
            }

        }

        private void OnDestroy()
        {
            _model.OnChanged -= OnSoundSettingChanged;
        }
    }
}
