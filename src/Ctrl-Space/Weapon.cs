using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class RocketWeapon : GameObject
    {
        public RocketWeapon(Vector2 position, float rotation)
        {
            Position = position;
            Size = 5;
            Speed = new Vector2(0.1f * (float)Math.Sin(rotation), -0.1f * (float)Math.Cos(rotation));
            Rotation = rotation;
        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D GetTexture(TextureManager textureManager)
        {
            return textureManager.RocketTexture;
        }
    }
}
