using Ctrl_Space.GameClasses.Bullets;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.GameClasses.Weapon
{
    class RocketLauncher : WeaponBase
    {
        public RocketLauncher(GameObject owner) : base(owner) { }

        public override void Shoot(World world)
        {
            Rocket rocket1 = new Rocket(Owner.Position + new Vector2(-40f * Maf.Cos(Owner.Rotation), -40f * Maf.Sin(Owner.Rotation)), Owner.Speed, Owner.Rotation);
            Rocket rocket2 = new Rocket(Owner.Position + new Vector2(40f * Maf.Cos(Owner.Rotation), 40f * Maf.Sin(Owner.Rotation)), Owner.Speed, Owner.Rotation);
            world.Add(rocket1);
            world.Add(rocket2);
        }
    }
}
