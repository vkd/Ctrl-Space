using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space
{
    class RocketWeapon : GameObject
    {
        public RocketWeapon(Vector2 position, Vector2 speed, float rotation)
        {
            Position = position;
            Size = 16;
            Speed = speed + new Vector2(7f * (float)Math.Sin(rotation), -7f * (float)Math.Cos(rotation));
            Rotation = rotation;
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.RocketTexture;
        }
    }
}
