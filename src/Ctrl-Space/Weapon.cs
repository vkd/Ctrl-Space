using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class Weapon : GameObject
    {
        public Weapon(Vector2 position)
            : base(position)
        {

        }
    }

    class RocketWeapon : Weapon
    {
        public RocketWeapon(Vector2 position, float rotation)
            : base(position)
        {
            Size = 5;
            Speed = new Vector2(0.1f * (float)Math.Sin(rotation), -0.1f * (float)Math.Cos(rotation));
            Rotation = rotation;
        }
    }
}
