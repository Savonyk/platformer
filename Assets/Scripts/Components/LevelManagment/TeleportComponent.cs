﻿using System.Collections;
using UnityEngine;
using Scripts.Creatures.Hero;
using UnityEngine.InputSystem;

namespace Scripts.Components.LevelManagment
{
    class TeleportComponent:MonoBehaviour
    {
        [SerializeField] private Transform _transformDestination;
        [SerializeField] private float _alphaTime = 1;
        [SerializeField] private float _moveTime = 1;

        public void Teleport(GameObject target)
        {
            StartCoroutine(AnimateTeleport(target));
        }

        private IEnumerator AnimateTeleport(GameObject target)
        {
            var sprite = target.GetComponent<SpriteRenderer>();
            var input = target.GetComponent<PlayerInput>();


            SetLockInput(input, true);
            target.SetActive(false);
            yield return AlphaAnimation(sprite, 0);

            yield return MoveAnimation(target);

            target.SetActive(true);
            yield return AlphaAnimation(sprite, 1);
            SetLockInput(input, false);
        }

        private void SetLockInput(PlayerInput input, bool isLocked)
        {
            if (input != null)
            {
                input.enabled = !isLocked;
            }
        }

        private IEnumerator AlphaAnimation(SpriteRenderer sprite, float destAlpha)
        {
            var time = 0f;
            var spriteAlpha = sprite.color.a;
            while (time < _alphaTime)
            {
                time += Time.deltaTime;
                var progress = time / _alphaTime;
                var tmpAlpha = Mathf.Lerp(spriteAlpha, destAlpha, progress);

                var color = sprite.color;
                color.a = tmpAlpha;
                sprite.color = color;

                yield return null;
            }
        }

        private IEnumerator MoveAnimation(GameObject target)
        {
            var moveTime = 0.0f;
            while (moveTime < _moveTime)
            {
                moveTime += Time.deltaTime;

                var progress = moveTime / _moveTime;
                target.transform.position = Vector3.Lerp(target.transform.position,
                                                        _transformDestination.position,
                                                         progress);

                yield return null;
            }
        }
    }
}
