using Ctrl_Space.Gameplay.Bullets;
using Ctrl_Space.Helpers;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.Gameplay.Weapon
{
    class PlasmaGun : WeaponBase
    {
        public PlasmaGun(GameObject owner) : base(owner)
        {
            _shootingInterval = 10;
        }

        public override void Shoot(World world)
        {
            var kickRocket = 20f;
            var speedRocket = 14.9f;

            PlasmaBullet plasmaBullet = new PlasmaBullet()
            {
                Size = 10,
                Position = Owner.Position + kickRocket * new Vector2(Maf.Sin(Owner.Rotation), -Maf.Cos(Owner.Rotation)),
                Speed = Owner.Speed + speedRocket * new Vector2(Maf.Sin(Owner.Rotation), -Maf.Cos(Owner.Rotation))
            };
            world.Add(plasmaBullet);
        }
    }
}
