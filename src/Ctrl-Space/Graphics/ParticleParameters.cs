using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Graphics
{
    class ParticleParameters
    {
        public float[] Sizes;
        public Color[] Colors;
        public float[] Alphas;
        public Func<MetaTexture> TextureGetter;
        public float Duration;
    }
}
