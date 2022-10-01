using UnityEngine;
using UnityEngine.EventSystems;
using Scripts.Utils;

namespace Scripts.UI.Widgets
{
    public class ButtonSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] 
        private AudioClip _audioClip;

        private AudioSource _source;

        public void OnPointerClick(PointerEventData eventData)
        {
            if(_source == null)
            {
                _source = SfxAudioUtils.FindSfxSource();
            }

            _source.PlayOneShot(_audioClip);
        }
    }
}
