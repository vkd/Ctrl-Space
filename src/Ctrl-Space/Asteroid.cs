using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space
{
    class Asteroid : GameObject
    {
        public override Texture2D GetTexture()
        {
            return TextureManager.AsteroidTexture;
        }
    }
}
