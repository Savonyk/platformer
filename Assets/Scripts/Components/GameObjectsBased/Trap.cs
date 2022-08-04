using UnityEngine;
using Scripts.Components.LevelManagment;
using Scripts.Components.ColiderBased;
using Scripts.Components.Animations;

namespace Scripts.Components.GameObjectsBased
{
    class Trap : MonoBehaviour
    {
        [SerializeField] private ColiderCheckComponent _vision;
        [SerializeField] private ResetComponent _coolDown;
        [SerializeField] private SpriteAnimationsComponent _animation;

        public bool IsTouchingPlayer => _vision.IsTouching;

        private void Update()
        {
            if (!_vision.IsTouching || !_coolDown.IsReady) return;

            Shoot();

        }

        public void Shoot()
        {
            _coolDown.Reset();
            _animation.SetClip("startAttack");
        }
    }
}
