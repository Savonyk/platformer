using Scripts.Utils;
using UnityEngine;

namespace Scripts.Components.Audio
{
    public class PlaySfxClipComponent : MonoBehaviour
    {

        [SerializeField] 
        private AudioClip _sound;

        private AudioSource _source;

        public void Play()
        {
            if (_source == null)
                _source = SfxAudioUtils.FindSfxSource();

            _source.PlayOneShot(_sound);
        }
    }
}
