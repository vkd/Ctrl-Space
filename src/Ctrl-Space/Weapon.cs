using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class Weapon : GameObject
    {
        public float Frequency;
        public float Range;
        public DateTime TTL;

        public Weapon(Vector2 position)
            : base(position)
        {

        }
    }

    class RocketWeapon : Weapon
    {
        public RocketWeapon(Vector2 position)
            : base(position)
        {
            Speed = new Vector2(0.1f, 0.1f);
            Frequency = 10;
        }
    }
}
