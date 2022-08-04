using UnityEngine;
using System;

namespace Scripts.Components.Audio
{
    public class PlayClipComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private SoundData[] _sounds;

        public void Play(string id)
        {
            foreach (var sound in _sounds)
            {
                if(sound.Id == id)
                {
                    _source.PlayOneShot(sound.Clip);
                    return;
                }
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
