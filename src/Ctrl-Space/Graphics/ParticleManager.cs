using Microsoft.Xna.Framework;

namespace Ctrl_Space.Graphics
{
    class ParticleManager
    {
        public static readonly ParticleParameters EngineFire = new ParticleParameters()
        {
            Duration = 20f,
            TextureGetter = () => TextureManager.SimpleGlowTexture,
            Colors = new Color[] { Color.Orange, Color.Red },
            Alphas = new float[] { 1f, 0f },
            Sizes = new float[] { 32f, 0f }
        };

        public static readonly ParticleParameters RocketFire = new ParticleParameters()
        {
            Duration = 20f,
            TextureGetter = () => TextureManager.SimpleGlowTexture,
            Colors = new Color[] { Color.Orange, Color.Red },
            Alphas = new float[] { 1f, 0f },
            Sizes = new float[] { 16f, 0f }
        };

        public static readonly ParticleParameters PlasmaTrail = new ParticleParameters()
        {
            Duration = 20f,
            TextureGetter = () => TextureManager.SimpleGlowTexture,
            Colors = new Color[] { Color.RoyalBlue, Color.DarkBlue },
            Alphas = new float[] { 1f, 0f },
            Sizes = new float[] { 16f, 0f }
        };

        public static readonly ParticleParameters Explosion = new ParticleParameters()
        {
            Duration = 40f,
            TextureGetter = () => TextureManager.SimpleGlowTexture,
            Colors = new Color[] { Color.Orange, Color.Red },
            Alphas = new float[] { 1f, 0f },
            Sizes = new float[] { 48f, 0f }
        };
    }
}
