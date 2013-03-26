using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space
{
    public class GameObject
    {
        public Vector2 Position;
        public Vector2 Speed;
        public float Size;
        
        public int Radius;

        public float Rotation;
        public float RotationSpeed;
        
        public Vector2 Origin;
        public Vector2 Scale;

        public Texture2D Texture;

        public BoundingBox BB;

        public GameObject()
        {

        }

        public GameObject(Vector2 position)
        {
            Position = position;
            BB = new BoundingBox(
                new Vector3(Position.X - Size / 2, Position.Y - Size / 2, 0),
                new Vector3(Position.X + Size / 2, Position.Y + Size / 2, 0));
        }

        public GameObject(float size, Vector2 position)
        {
            Size = size;
            Position = position;
            BB = new BoundingBox(
                new Vector3(Position.X - Size / 2, Position.Y - Size / 2, 0),
                new Vector3(Position.X + Size / 2, Position.Y + Size / 2, 0));
        }

        public GameObject(float size, Vector2 position, Vector2 speed)
        {
            Size = size;
            Position = position;
            Speed = speed;
            BB = new BoundingBox(
                new Vector3(Position.X - Size / 2, Position.Y - Size / 2, 0),
                new Vector3(Position.X + Size / 2, Position.Y + Size / 2, 0));
        }

        public GameObject(float size, Vector2 position, Vector2 speed, float rotation)
        {
            Size = size;
            Position = position;
            Speed = speed;
            Rotation = rotation;
            BB = new BoundingBox(
                new Vector3(Position.X - Size / 2, Position.Y - Size / 2, 0),
                new Vector3(Position.X + Size / 2, Position.Y + Size / 2, 0));
        }

        public GameObject(float size, Vector2 position, Vector2 speed, float rotation, float rotationSpeed)
        {
            Size = size;
            Position = position;
            Speed = speed;
            Rotation = rotation;
            RotationSpeed = rotation;
            BB = new BoundingBox(
                new Vector3(Position.X - Size / 2, Position.Y - Size / 2, 0),
                new Vector3(Position.X + Size / 2, Position.Y + Size / 2, 0));
        }
    }
}
