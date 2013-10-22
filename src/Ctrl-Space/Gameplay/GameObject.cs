using System;
using System.Collections.Generic;
using Ctrl_Space.Graphics;
using Ctrl_Space.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Gameplay
{
    abstract class GameObject
    {
        public int Id = -1;
        public int HP = 0;
        public int MaxHP = 100;
        public bool DrawHP = false;

        public Vector2 Position;
        public Vector2 Speed;
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

        public void ResetGameObject()
        {
            IsDestroyed = false;
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

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 offset)
        {
            var tex = GetTexture();
            var s = Math.Max(tex.Region.Width, tex.Region.Height);
            spriteBatch.Draw(tex.Texture, Position + offset, tex.Region, Color * Alpha, Rotation, new Vector2(tex.Region.Width / 2, tex.Region.Height / 2), Size / s, SpriteEffects.None, 0f);
        }

        public abstract MetaTexture GetTexture();
    }
}
