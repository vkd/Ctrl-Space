using Ctrl_Space.GameClasses.Bullets;
using Ctrl_Space.Input;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.GameClasses.Weapon
{
    class RocketLauncher : WeaponBase
    {
        public RocketLauncher(GameObject owner) : base(owner) { }

        private bool _fire = false;
        private int _shootCycle = 50;
        private int _currentCycle = 0;

        public override void Shoot(InputDigitalState state, World world)
        {
            if (state == InputDigitalState.Pressed)
            {
                Rocket rocket1 = new Rocket(Owner.Position + new Vector2(-40f * Maf.Cos(Owner.Rotation), -40f * Maf.Sin(Owner.Rotation)), Owner.Speed, Owner.Rotation);
                Rocket rocket2 = new Rocket(Owner.Position + new Vector2(40f * Maf.Cos(Owner.Rotation), 40f * Maf.Sin(Owner.Rotation)), Owner.Speed, Owner.Rotation);
                world.Add(rocket1);
                world.Add(rocket2);
            }
        }
    }
}
