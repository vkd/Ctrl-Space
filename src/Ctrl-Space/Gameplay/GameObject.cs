using System;
using System.Collections.Generic;
using Ctrl_Space.Graphics;
using Ctrl_Space.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ctrl_Space.Helpers;

namespace Ctrl_Space.Gameplay
{
    abstract class GameObject
    {
        public int Id = -1;
        public int HP = 0;
        public int MaxHP = 100;
        public bool DrawHP = false;

        public Vector2 Position;
        private Vector2 _speed;
        public float Size;
        public float Mass = 1f;
        public float Rotation;
        public float RotationSpeed;
        public Color Color = Color.White;
        public float Alpha = 1f;
        public bool IsDestroyed = false;
        public readonly List<Collision> Collisions = new List<Collision>();

        public GameObject()
        {
        }

        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public void ResetGameObject()
        {
            IsDestroyed = false;
            for (int i = 0; i < Collisions.Count; i++)
                CollisionPool.Instance.PutObject(Collisions[i]);
            Collisions.Clear();
        }

        public virtual void Update(World world, Particles particles)
        {
            Position += Speed;
            Position = new Vector2(
                (Position.X + Game.WorldWidth) % Game.WorldWidth,
                (Position.Y + Game.WorldHeight) % Game.WorldHeight);
            Rotation += RotationSpeed;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 offset, Camera camera, DebugGeometry debugGeometry)
        {
            var tex = GetTexture();
            var s = Math.Max(tex.Region.Width, tex.Region.Height);
            spriteBatch.Draw(tex.Texture, Position + offset, tex.Region, Color * Alpha, Rotation, new Vector2(tex.Region.Width / 2, tex.Region.Height / 2), Size / s, SpriteEffects.None, 0f);

            // DrawHP
            if (DrawHP)
            {
                Vector2 start = new Vector2(-10f, -Size / 2 - 5f);
                start = new Vector2(start.X * Maf.Cos(-camera.FollowedObject.Rotation) + start.Y * Maf.Sin(-camera.FollowedObject.Rotation), -start.X * Maf.Sin(-camera.FollowedObject.Rotation) + start.Y * Maf.Cos(-camera.FollowedObject.Rotation));
                debugGeometry.DrawLine(Position + start + offset, 20f, camera.FollowedObject.Rotation, Color.Red);
                debugGeometry.DrawLine(Position + start + offset, 20f * HP / MaxHP, camera.FollowedObject.Rotation, Color.Green);
            }
        }

        public abstract MetaTexture GetTexture();
    }
}
