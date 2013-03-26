using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class SpaceObject : GameObject
    {
        public SpaceObject(Vector2 position)
            : base(position)
        {

        }
    }

    class Asteroid : SpaceObject
    {
        private const float _mediumAsteroidSize = 48;

        public Asteroid(Vector2 position)
            : base(position)
        {

        }
    }
}
