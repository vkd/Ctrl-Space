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
        private Vector2 _acceleration = Vector2.Zero;

        public Rocket()
        {
        }

        public void Reset(Vector2 position, Vector2 speed, float rotation)
        {
            State = 1.0f;
            Position = position;
            Size = 16;
            _acceleration = .5f * new Vector2(Maf.Sin(rotation), -Maf.Cos(rotation));
            Speed = speed; // +new Vector2(7f * Maf.Sin(rotation), -7f * Maf.Cos(rotation));
            Rotation = rotation;
        }

        public override MetaTexture GetTexture()
        {
            return TextureManager.RocketTexture;
        }

        public override void Update(World world, Particles particles)
        {
            
            Speed += _acceleration;
            particles.Emit(ParticleManager.RocketFire, Position - new Vector2(10f * Maf.Sin(Rotation), -10f * Maf.Cos(Rotation)), Chaos.GetVector2InCircle() + Speed);
            foreach (var col in Collisions)
                Collided(col.GameObject, world, particles);
            base.Update(world, particles);
            State -= .01f;
            if (State < 0f)
                IsDestroyed = true; 
        }

        public void Collided(GameObject go, World world, Particles particles)
        {
            IsDestroyed = true;
        }
    }
}
