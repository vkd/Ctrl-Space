using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space
{
    class SpeedBonus : GameObject
    {
        public SpeedBonus(Vector2 position)
        {
            Speed = Vector2.Zero;
            Size = 15;
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.SpeedBonusTexture;
        }
    }
}
