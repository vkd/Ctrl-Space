using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class GameObject
    {
        public Vector2 Position;
        public Vector2 Speed;
        public float Rotation;
        public float RotationSpeed;
        public float Size;
    }

    class Weapon : GameObject
    {
        public float Frequency;
        public float Range;
        public DateTime TTL;
    }

    class Bonus : GameObject
    {
        public DateTime TTL;

        public Bonus()
        {
            Speed.X = 0;
            Speed.Y = 0;
        }
    }

    class SpeedBonus : Bonus
    {
        public int GiveSpeed;

        public SpeedBonus() : base()
        {
            Size = 15;
        }
    }
}
