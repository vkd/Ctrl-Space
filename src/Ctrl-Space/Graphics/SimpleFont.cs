using Ctrl_Space.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace MonogameTest.Graphics
{
    class SimpleFont
    {
        private readonly Encoding _encoding;
        public SimpleFont()
        {
            _encoding = Encoding.GetEncoding(1251);
        }
        public void DrawText(SpriteBatch spriteBatch, string value, float size, Vector2 position, Color color)
        {
            var data = _encoding.GetBytes(value);
            foreach (var c in data)
            {
                var mt = TextureManager.FontTexture[c];
                spriteBatch.Draw(mt.Texture, position + Vector2.One, mt.Region, Color.Black, 0f, Vector2.Zero, size / mt.Region.Width, SpriteEffects.None, 0f);
                spriteBatch.Draw(mt.Texture, position, mt.Region, color, 0f, Vector2.Zero, size / mt.Region.Width, SpriteEffects.None, 0f);
                position.X += size;
            }
        }
    }
}
