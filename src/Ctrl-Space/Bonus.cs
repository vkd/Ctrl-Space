using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class Bonus : GameObject
    {
        public Bonus(Vector2 position)
            : base(position)
        {

        }
    }

    class SpeedBonus : Bonus
    {
        public SpeedBonus(Vector2 position)
            : base(position)
        {
            Speed = Vector2.Zero;
            Size = 15;
        }
    }
}
