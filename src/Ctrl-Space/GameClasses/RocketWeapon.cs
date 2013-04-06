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
            Speed = speed + new Vector2(7f * Maf.Sin(rotation), -7f * Maf.Cos(rotation));
            Rotation = rotation;
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.RocketTexture;
        }

        public override void Update()
        {
            base.Update();
            Rotation = (float)Math.Atan2(Speed.X, -Speed.Y);
        }
    }
}
