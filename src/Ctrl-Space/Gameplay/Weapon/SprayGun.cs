using Ctrl_Space.Gameplay.Bullets;
using Microsoft.Xna.Framework;
using Ctrl_Space.Helpers;

namespace Ctrl_Space.Gameplay.Weapon
{
    class SprayGun : WeaponBase
    {
        public SprayGun(GameObject owner) : base(owner)
        {
            _shootingInterval = 20;
            Speed = 25;
        }

        public override void Shoot(World world)
        {
            var kickRocket = 40f;
            int countBullet = 7;

            for (int i = -(countBullet / 2); i < (countBullet / 2) + 1; ++i)
            {
                float rot = Owner.Rotation + i * 0.1f;

                PlasmaBullet plasmaBullet = Game.Objects.CreatePlasmaBullet();
                plasmaBullet.Size = 10;
                plasmaBullet.Position = Owner.Position + kickRocket * new Vector2(Maf.Sin(rot), -Maf.Cos(rot));
                plasmaBullet.Speed = Owner.Speed + Speed * new Vector2(Maf.Sin(rot), -Maf.Cos(rot));
                world.Add(plasmaBullet);
            }
        }
    }
}
