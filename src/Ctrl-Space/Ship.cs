using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space
{
    public class Ship : GameObject
    {
        public Ship(Vector2 position) : base()
        {
            Mass = 20f;
            Position = position;
            Size = 48;
        }

        public void Rotate(float rotationSpeed)
        {
            Rotation += rotationSpeed;
        }

        public void Strafe(float strafeStep)
        {
            Speed.X += strafeStep * Maf.Cos(Rotation);
            Speed.Y += strafeStep * Maf.Sin(Rotation);
        }

        public void SpeedUp(float acceleration)
        {
            Speed.X += acceleration * Maf.Sin(Rotation);
            Speed.Y -= acceleration * Maf.Cos(Rotation);
        }

        public override void Update()
        {
            base.Update();
            Speed *= .99f;
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.ShipOffTexture;
        }
    }
}
