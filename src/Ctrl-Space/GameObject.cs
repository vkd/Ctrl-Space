using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space
{
    public abstract class GameObject
    {
        public Vector2 Position;
        public Vector2 Speed;
        public float Size;
        public float Mass = 1f;
        public float Rotation;
        public float RotationSpeed;

        public GameObject()
        {

        }

        public virtual void Update()
        {
            Position += Speed;
            Position = new Vector2(
                (Position.X + Game1.WorldWidth) % Game1.WorldWidth,
                (Position.Y + Game1.WorldHeight) % Game1.WorldHeight);
            Rotation += RotationSpeed;
        }

        public void Draw(SpriteBatch spriteBatch, TextureManager textureManager)
        {
            var tex = GetTexture(textureManager);
            var s = Math.Max(tex.Width, tex.Height);
            spriteBatch.Draw(tex, Position, null, Color.White, Rotation, new Vector2(tex.Width / 2, tex.Height / 2), Size / s, SpriteEffects.None, 0f);
        }

        public abstract Texture2D GetTexture(TextureManager textureManager);
    }
}
