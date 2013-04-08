using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space
{
    class ParticleParameters
    {
        public float[] Sizes;
        public Color[] Colors;
        public float[] Alphas;
        public Func<Texture2D> TextureGetter;
        public float Duration;
    }
}
