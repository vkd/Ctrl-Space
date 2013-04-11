using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ctrl_Space.GameClasses.Weapon;

namespace Ctrl_Space
{
    class Ship : GameObject
    {
        private WeaponBase _weapon;
        private WeaponBase _weaponAlt;

        public Ship(Vector2 position)
            : base()
        {
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

        public void Shoot(World world)
        {
            _weapon.Shoot(world);
        }

        public void ShootAlt(World world)
        {
            _weaponAlt.Shoot(world);
        }

        public override void Update()
        {
            base.Update();
            Speed *= .99f;
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.ShipOffTexture;
        }
    }
}
