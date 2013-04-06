using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class PlasmaBullet : GameObject
    {
        public override Texture2D GetTexture()
        {
            return TextureManager.PlasmaBulletTexture;
        }
    }
}
