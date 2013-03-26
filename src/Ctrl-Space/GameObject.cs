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
}
