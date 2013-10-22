using Ctrl_Space.Gameplay.Bullets;
using Ctrl_Space.Graphics;
using Ctrl_Space.Helpers;
using Ctrl_Space.Physics;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Gameplay
{
    class Asteroid : GameObject
    {
        public Asteroid()
        {
            DrawHP = true;
        }

        public override MetaTexture GetTexture()
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
                    particles.Emit(ParticleManager.Explosion, (go.Position + Position) / 2f, Chaos.GetVector2InCircle(3f));
                if (go is Rocket)
                    HP -= 7;
                else
                    HP -= 3;
                if (HP <= 0)
                    IsDestroyed = true;
            }

            if (go is Asteroid)
            {
                if (collision.Time > 0)
                    Response.Apply(this, collision);
                collision.GameObject.Collisions.RemoveAll(c => c.GameObject == this);
            }
        }
    }
}
