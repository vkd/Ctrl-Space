using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrl_Space
{
    class Ship : GameObject
    {
        public Ship()
            : base()
        {

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
