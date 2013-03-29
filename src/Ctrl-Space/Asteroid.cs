using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class Asteroid : GameObject
    {
        public Asteroid()
        {

        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D GetTexture(TextureManager textureManager)
        {
            return textureManager.AsteroidTexture;
        }
    }
}
