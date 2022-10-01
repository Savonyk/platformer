using UnityEngine;
using UnityEngine.UI;
using Scripts.Model.Data;
using Scripts.Utils;
using System.Collections;

namespace Scripts.UI.HUD.Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [SerializeField]
        private Text _printedText;
        [SerializeField]
        private GameObject _container;
        [SerializeField]
        private Animator _animator;
        [Space] 
        [SerializeField]
        private float printingSpeed;
        [Space]
        [Header("Audio")]
        [SerializeField]
        private AudioClip _opening;
        [SerializeField]
        private AudioClip _printing;
        [SerializeField]
        AudioClip _closed;

        private DialogData _data;
        private int _currentPhrase;
        private Coroutine _typingRoutine;
        private AudioSource _sfxSource;
        private static readonly int IsOpenedKey = Animator.StringToHash("isOpened");

        private void Start()
        {
            _sfxSource = SfxAudioUtils.FindSfxSource();
        }

        public void ShowDialog(DialogData data)
        {
            _data = data;

            _currentPhrase = 0;
            _typingRoutine = null;
            _printedText.text = string.Empty;
            _container.SetActive(true);
            _animator.SetBool(IsOpenedKey, true);
            _sfxSource.PlayOneShot(_opening);
        }

        private void OpenAnimationCompleted()
        {
            _typingRoutine = StartCoroutine(Co_TypePhrase());
        }

        private IEnumerator Co_TypePhrase()
        {
            _printedText.text = string.Empty;
            var phrase = _data.Phrases[_currentPhrase];

            foreach (var letter in phrase)
            {
                _printedText.text += letter;
                _sfxSource.PlayOneShot(_printing);
                yield return new WaitForSeconds(printingSpeed);
            }
            _typingRoutine = null;
        }

        private void CloseAnimationCompleted()
        {
            _container.SetActive(false);
        }

        public void OnSkip()
        {
            if (_typingRoutine == null) return;

            StopTypeAnimation();
            _printedText.text = _data.Phrases[_currentPhrase];
        }

        public void OnContinue()
        {
            StopTypeAnimation();
            _currentPhrase++;

            var isDialogCompleted = _currentPhrase >= _data.Phrases.Length;

            if (isDialogCompleted)
            {
                HideDialogBlock();
            }
            else
            {
                OpenAnimationCompleted();
            }
        }

        private void HideDialogBlock()
        {
            _printedText.text = string.Empty;
            _animator.SetBool(IsOpenedKey, false);
            _sfxSource.PlayOneShot(_closed);
        }

        private void StopTypeAnimation()
        {
            if(_typingRoutine != null)
            {
                StopCoroutine(_typingRoutine);
            }
            _typingRoutine = null;
        }
    }
}
