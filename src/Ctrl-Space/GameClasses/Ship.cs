using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space
{
    class Ship : GameObject
    {
        public Ship(Vector2 position)
            : base()
        {
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
            var kickRocket = 20f;
            var speedRocket = 14.9f;

            PlasmaBullet plasmaBullet = new PlasmaBullet()
            {
                Size = 10,
                Position = Position + kickRocket * new Vector2(Maf.Sin(Rotation), -Maf.Cos(Rotation)),
                Speed = Speed + speedRocket * new Vector2(Maf.Sin(Rotation), -Maf.Cos(Rotation))
            };
            world.Add(plasmaBullet);
        }

        public void ShootAlt(World world)
        {
            RocketWeapon rocket1 = new RocketWeapon(Position + new Vector2(-40f * Maf.Cos(Rotation), -40f * Maf.Sin(Rotation)), Speed, Rotation);
            RocketWeapon rocket2 = new RocketWeapon(Position + new Vector2(40f * Maf.Cos(Rotation), 40f * Maf.Sin(Rotation)), Speed, Rotation);
            world.Add(rocket1);
            world.Add(rocket2);
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
