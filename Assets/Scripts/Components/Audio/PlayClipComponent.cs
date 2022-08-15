using UnityEngine;
using System;
using Scripts.Utils;

namespace Scripts.Components.Audio
{
    public class PlayClipComponent : MonoBehaviour
    {
        [SerializeField] private SoundData[] _sounds;
        private AudioSource _source;

        public void Play(string id)
        {
            foreach (var sound in _sounds)
            {
                if (sound.Id != id) continue;

                if(_source == null)
                {
                    _source = SfxAudioUtils.FindSfxSource();
                }

                _source.PlayOneShot(sound.Clip);
                break;

            }
        }

    }

    [Serializable]
    public class SoundData
    {
        [SerializeField] private string _id;
        [SerializeField] private AudioClip _clip;

        public string Id => _id;
        public AudioClip Clip => _clip;
    }
}
