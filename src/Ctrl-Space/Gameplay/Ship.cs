using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ctrl_Space.GameClasses.Weapon;
using Ctrl_Space.Input;

namespace Ctrl_Space
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

        public override void Update()
        {
            _weapon.Update(_world);
            _weaponAlt.Update(_world);
            base.Update();
            Speed *= .99f;
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.ShipOffTexture;
        }
    }
}
