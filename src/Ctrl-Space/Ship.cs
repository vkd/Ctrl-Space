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
        private const float _mediumShipSize = 48; 

        public Ship(Vector2 position)
            : base(position)
        {
            Size = _mediumShipSize;
        }

        public void Rotate(float rotationSpeed)
        {
            Rotation += rotationSpeed;
        }

        public void SpeedUp(float acceleration)
        {
            Speed.X += (float)(acceleration * Math.Sin(Rotation));
            Speed.Y -= (float)(acceleration * Math.Cos(Rotation));
        }

        public void SpeedDown(float acceleration)
        {
            Speed.X -= (float)(acceleration * Math.Sin(Rotation));
            Speed.Y += (float)(acceleration * Math.Cos(Rotation));
        }
    }
}
