using UnityEngine;
using System;
using UnityEngine.Events;

namespace Scripts.Components.Animations
{
    [RequireComponent(typeof(SpriteRenderer))]

    public class SpriteAnimationsComponent : MonoBehaviour
    {
        [SerializeField] [Range(1,30)] private int _frameRate = 10;
        [SerializeField] private UnityEvent<string> _onComplete;
        [SerializeField] private AnimationClip[] _clips;

        private SpriteRenderer _render;

        private float _secondsPerFrame;
        private float _nextFrameTime;
        private int _currentFrame;
        private bool _isPlaying = true;

        private int _currentClip;

        private void Start()
        {
            _render = GetComponent<SpriteRenderer>();
            _secondsPerFrame = 1f / _frameRate;

            StartAnimation();
        }

        private void OnBecameVisible()
        {
            enabled = _isPlaying;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        private void OnEnable()
        {
            _nextFrameTime = Time.time + _secondsPerFrame;
        }

        public void SetClip(string ClipName)
        {
            for( var i = 0; i < _clips.Length; i++)
            {
                if (_clips[i].Name == ClipName)
                {
                    _currentClip = i;
                    StartAnimation();
                    return;
                }
            }

            enabled = _isPlaying = false;
        }

        private void StartAnimation()
        {
            _nextFrameTime = Time.time + _secondsPerFrame;
            enabled = _isPlaying = true;
            _currentFrame = 0;
        }

        private void Update()
        {
            if (_nextFrameTime > Time.time) return;

            var clip = _clips[_currentClip];
            
            if(_currentFrame >= clip.Sprites.Length)
            {
                if (clip.IsLoop)
                {
                    _currentFrame = 0;
                }
                else
                {
                    enabled = _isPlaying = clip.AllowNextClip;
                    clip.OnComplete?.Invoke();
                    _onComplete?.Invoke(clip.Name);
                    if (clip.AllowNextClip)
                    {
                        _currentFrame = 0;
                        _currentClip = (int)Mathf.Repeat(_currentClip + 1, _clips.Length);
                    }
                }
                return;
            }

            _render.sprite = clip.Sprites[_currentFrame];
             
            _nextFrameTime += _secondsPerFrame;
            _currentFrame++;
        }
    }

    [Serializable]

    public class AnimationClip 
    {
        [SerializeField] private string _name;

        [SerializeField] private bool _loop;

        [SerializeField] private Sprite[] _sprites;

        [SerializeField] private bool _allowNextClip;

        [SerializeField] private UnityEvent _onComplete;

        public string Name => _name;
        public Sprite[] Sprites => _sprites;
        public bool IsLoop => _loop;
        public bool AllowNextClip => _allowNextClip;
        public UnityEvent OnComplete => _onComplete;
    }
}