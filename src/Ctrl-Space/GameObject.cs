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
        public int Radius;
        public Vector2 Speed;
        public float Rotation;
        public float RotationSpeed;
        public float Size;

        //Бокс для расчета столкновений
        public BoundingBox BB;

        public GameObject()
        {

        }

        public GameObject(float size)
        {
            Size = size;
            BB = new BoundingBox(
                new Vector3(Position.X - Size / 2, Position.Y - Size / 2, 0),
                new Vector3(Position.X + Size / 2, Position.Y + Size / 2, 0));
        }
    }

    class Weapon : GameObject
    {
        public float Frequency;
        public float Range;
        public DateTime TTL;

        public Weapon()
            : base(20)
        {

        }
    }

    class RocketWeapon : Weapon
    {
        public RocketWeapon()
            : base()
        {
            Speed = new Vector2(0.1f, 0.1f);
            Frequency = 10;
        }
    }

    class Bonus : GameObject
    {
        public DateTime TTL;

        public Bonus(float size) : base(size)
        {
            Speed.X = 0;
            Speed.Y = 0;
        }
    }

    class SpeedBonus : Bonus
    {
        public int GiveSpeed;

        public SpeedBonus() : base(15)
        {

        }
    }
}
