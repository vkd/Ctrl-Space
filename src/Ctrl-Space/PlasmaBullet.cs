using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrl_Space
{
    class PlasmaBullet : GameObject
    {
        public override Microsoft.Xna.Framework.Graphics.Texture2D GetTexture(TextureManager textureManager)
        {
            return textureManager.PlasmaBulletTexture;
        }
    }
}
