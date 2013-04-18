using Ctrl_Space.Gameplay.Weapon;
using Ctrl_Space.Input;
using Ctrl_Space.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Gameplay
{
    class Ship : GameObject
    {
        private World _world;

        private WeaponBase _weapon;
        private WeaponBase _weaponAlt;

        public Ship(Vector2 position, World world)
            : base()
        {
            _world = world;

            _weapon = new PlasmaGun(this);
            _weaponAlt = new RocketLauncher(this);

            Mass = 20f;
            Position = position;
            Size = 48;
        }

        public void Rotate(float rotationSpeed)
        {
            Rotation += rotationSpeed;
        }

        public void Strafe(float strafeStep)
        {
            Speed.X += strafeStep * Maf.Cos(Rotation);
            Speed.Y += strafeStep * Maf.Sin(Rotation);
        }

        public void SpeedUp(float acceleration)
        {
            Speed.X += acceleration * Maf.Sin(Rotation);
            Speed.Y -= acceleration * Maf.Cos(Rotation);
        }

        public void Shoot(InputDigitalState state)
        {
            if (state == InputDigitalState.Pressed)
                _weapon.On();
            else
                _weapon.Off();
        }

        public void ShootAlt(InputDigitalState state)
        {
            if (state == InputDigitalState.Pressed)
                _weaponAlt.On();
            else
                _weaponAlt.Off();
        }

        public override void Update(World world, Particles particles)
        {
            _weapon.Update(_world);
            _weaponAlt.Update(_world);
            foreach (var col in Collisions)
                Collided(col, world, particles);
            base.Update(world, particles);
            Speed *= .99f;
        }

        private void Collided(Collision col, World world, Particles particles)
        {
            if (col.GameObject is SpeedBonus)
            {
                Speed += new Vector2(10f * Maf.Sin(Rotation), -10f * Maf.Cos(Rotation));
            }
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.ShipOffTexture;
        }
    }
}
