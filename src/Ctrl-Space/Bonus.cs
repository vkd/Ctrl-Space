using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrl_Space
{
    class Bonus : GameObject
    {
        public DateTime TTL;

        public Bonus(float size)
            : base(size)
        {
            Speed.X = 0;
            Speed.Y = 0;
        }
    }

    class SpeedBonus : Bonus
    {
        public int GiveSpeed;

        public SpeedBonus()
            : base(15)
        {

        }
    }
}
