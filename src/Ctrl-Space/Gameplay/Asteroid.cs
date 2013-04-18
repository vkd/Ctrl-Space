using Ctrl_Space.Gameplay.Bullets;
using Ctrl_Space.Graphics;
using Ctrl_Space.Physics;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Gameplay
{
    class Asteroid : GameObject
    {
        public override Texture2D GetTexture()
        {
            return TextureManager.AsteroidTexture;
        }

        public override void Update(World world, Particles particles)
        {
            foreach (var col in Collisions)
                Collided(col, world, particles);
            base.Update(world, particles);
        }

        public void Collided(Collision collision, World world, Particles particles)
        {
            var go = collision.GameObject;

            if (go is Rocket || go is PlasmaBullet)
            {
                for (int h = 0; h < 100; h++)
                    particles.Emit(ParticleManager.Explosion, (go.Position + Position) / 2f, 3f * Chaos.GetFloat() * Chaos.GetVector2());
                Size -= 20f;
                if (Size < 40f)
                    IsDestroyed = true;
            }

            if (go is Asteroid)
            {
                if(collision.Time > 0)
                    Response.Apply(this, collision);
                collision.GameObject.Collisions.RemoveAll(c => c.GameObject == this);
            }
        }
    }
}
