using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space
{
    abstract class GameObject
    {
        public Vector2 Position;
        public Vector2 Speed;
        public float Size;
        public float Mass = 1f;
        public float Rotation;
        public float RotationSpeed;
        public Color Color = Color.White;
        public float Alpha = 1f;
        private Animation _animation;
        public bool IsDestroyed = false;

        public GameObject()
        {
            _animation = new Animation();
        }

        public virtual void Update()
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
            var rect = _animation.GetAnimation(gameTime, tex);
            var s = Math.Max(rect.Width, rect.Height);
            spriteBatch.Draw(tex, Position + offset, rect, Color * Alpha, Rotation, new Vector2(rect.Width / 2, rect.Height / 2), Size / s, SpriteEffects.None, 0f);
        }

        public virtual void Collided(GameObject go, World world, Particles particles)
        {
        }

        public abstract Texture2D GetTexture();
    }
}
