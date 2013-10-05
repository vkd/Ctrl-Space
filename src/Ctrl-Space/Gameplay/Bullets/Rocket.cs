using System;
using Ctrl_Space.Graphics;
using Ctrl_Space.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Gameplay.Bullets
{
    class Rocket : GameObject
    {
        private float State = 1.0f;

        public Rocket()
        {
        }

        public void Reset(Vector2 position, Vector2 speed, float rotation)
        {
            State = 1.0f;
            Position = position;
            Size = 16;
            Speed = speed + new Vector2(7f * Maf.Sin(rotation), -7f * Maf.Cos(rotation));
            Rotation = rotation;
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.RocketTexture;
        }

        public override void Update(World world, Particles particles)
        {
            foreach (var col in Collisions)
                Collided(col.GameObject, world, particles);
            base.Update(world, particles);
            Rotation = (float)Math.Atan2(Speed.X, -Speed.Y);
            State -= .01f;
            if (State < 0f)
                IsDestroyed = true;
            particles.Emit(ParticleManager.RocketFire, Position - new Vector2(10f * Maf.Sin(Rotation), -10f * Maf.Cos(Rotation)), 1f * Chaos.GetFloat() * Chaos.GetVector2());
        }

        public void Collided(GameObject go, World world, Particles particles)
        {
            IsDestroyed = true;
        }
    }
}
