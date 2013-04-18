using Ctrl_Space.GameClasses.Bullets;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
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
                Collided(col.GameObject, world, particles);
            base.Update(world, particles);
        }

        public void Collided(GameObject go, World world, Particles particles)
        {
            ParticleParameters ppExplosion = new ParticleParameters()
            {
                Duration = 40f,
                TextureGetter = () => TextureManager.SimpleGlowTexture,
                Colors = new Color[] { Color.Orange, Color.Red },
                Alphas = new float[] { 1f, 0f },
                Sizes = new float[] { 48f, 0f }
            };

            if (go is Rocket || go is PlasmaBullet)
            {
                for (int h = 0; h < 100; h++)
                    particles.Emit(ppExplosion, (go.Position + Position) / 2f, 3f * Chaos.GetFloat() * Chaos.GetVector2());
                Size -= 20f;
                if (Size < 40f)
                    IsDestroyed = true;
            }
        }
    }
}
