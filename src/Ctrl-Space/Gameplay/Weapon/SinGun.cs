using System;
using Ctrl_Space.Gameplay.Bullets;
using Microsoft.Xna.Framework;
using Ctrl_Space.Helpers;

namespace Ctrl_Space.Gameplay.Weapon
{
    class SinGun : WeaponBase
    {
        public SinGun(GameObject owner)
            : base(owner)
        {
            _shootingInterval = 5;
            Speed = 15;
        }

        public override void Shoot(World world)
        {
            var kickRocket = 30f;

            SinPlasmaBullet plasmaBullet = Game.Objects.CreateSinPlasmaBullet();
            plasmaBullet.Size = 10;
            plasmaBullet.Position = Owner.Position + kickRocket * new Vector2(Maf.Sin(Owner.Rotation), -Maf.Cos(Owner.Rotation));
            plasmaBullet.StartSpeed = Owner.Speed + Speed * new Vector2(Maf.Sin(Owner.Rotation), -Maf.Cos(Owner.Rotation));
            plasmaBullet.Speed = plasmaBullet.StartSpeed;
            world.Add(plasmaBullet);
        }
    }
}
