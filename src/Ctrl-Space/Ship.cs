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
            Position = position;
            Size = 48;
        }

        public void Rotate(float rotationSpeed)
        {
            Rotation += rotationSpeed;
        }

        public void Strafe(float strafeStep)
        {
            Speed.X += (float)(strafeStep * Math.Cos(Rotation));
            Speed.Y += (float)(strafeStep * Math.Sin(Rotation));
        }

        public void SpeedUp(float acceleration)
        {
            Speed.X += (float)(acceleration * Math.Sin(Rotation));
            Speed.Y -= (float)(acceleration * Math.Cos(Rotation));
        }

        public override void Update()
        {
            base.Update();
            Speed *= .99f;
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.ShipAnimation;
        }
    }
}
