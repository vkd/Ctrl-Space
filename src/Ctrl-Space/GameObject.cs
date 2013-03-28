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
        public float Mass = 1f;
        public float Rotation;
        public float RotationSpeed;

        public BoundingBox BB;

        #region Constructors

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
            RotationSpeed = rotationSpeed;
            BB = new BoundingBox(
                new Vector3(Position.X - Size / 2, Position.Y - Size / 2, 0),
                new Vector3(Position.X + Size / 2, Position.Y + Size / 2, 0));
        }

        #endregion

        #region Updaters

        public void Update()
        {
            Position += Speed;

            BB = new BoundingBox(
                new Vector3(Position.X - Size / 2, Position.Y - Size / 2, 0),
                new Vector3(Position.X + Size / 2, Position.Y + Size / 2, 0));
        }

        //Something not undestood
        public void UpdateWithNewPosition(Vector2 newPosition)
        {
            Position = newPosition;

            BB = new BoundingBox(
                new Vector3(Position.X - Size / 2, Position.Y - Size / 2, 0),
                new Vector3(Position.X + Size / 2, Position.Y + Size / 2, 0));
        }

        public void UpdateWithRotation()
        {
            Position += Speed;
            Rotation += RotationSpeed;

            BB = new BoundingBox(
                new Vector3(Position.X - Size / 2, Position.Y - Size / 2, 0),
                new Vector3(Position.X + Size / 2, Position.Y + Size / 2, 0));
        }

        #endregion

        #region Drawers

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, Rotation, new Vector2(Size / 2, Size / 2), Vector2.One, SpriteEffects.None, 0f);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Vector2 origin, Vector2 scale)
        {
            spriteBatch.Draw(texture, Position, null, Color.White, Rotation, origin, scale, SpriteEffects.None, 0f);
        }

        #endregion
    }
}
