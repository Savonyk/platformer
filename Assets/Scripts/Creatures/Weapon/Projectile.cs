using UnityEngine;

namespace Scripts.Creatures.Weapon
{
    class Projectile : BaseProjectile
    {
        protected override void Start()
        {
            base.Start();
            var force = new Vector2(DirectionX * Speed, 0f);
            Rigidbody.AddForce(force, ForceMode2D.Impulse);
        }

    }
}
