using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.GameClasses.Bullets
{
    class Rocket : GameObject
    {
        private float State = 1.0f;

        public Rocket(Vector2 position, Vector2 speed, float rotation)
        {
            Position = position;
            Size = 16;
            Speed = speed + new Vector2(7f * Maf.Sin(rotation), -7f * Maf.Cos(rotation));
            Rotation = rotation;
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.RocketTexture;
        }

        public override void Update()
        {
            base.Update();
            Rotation = (float)Math.Atan2(Speed.X, -Speed.Y);
            State -= .01f;
            if (State < 0f)
                IsDestroyed = true;
        }

        public override void Collided(GameObject go, World world, Particles particles)
        {
            IsDestroyed = true;
        }
    }
}
