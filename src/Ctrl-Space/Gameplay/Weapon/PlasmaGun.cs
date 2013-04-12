using Ctrl_Space.GameClasses.Bullets;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.GameClasses.Weapon
{
    class PlasmaGun : WeaponBase
    {
        public PlasmaGun(GameObject owner) : base(owner) { }

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
