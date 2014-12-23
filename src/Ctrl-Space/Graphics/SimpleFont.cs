using Ctrl_Space.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace MonogameTest.Graphics
{
    class SimpleFont
    {
        public void DrawText(SpriteBatch spriteBatch, string value, Vector2 position, Color color)
        {
            var data = Encoding.GetEncoding(1251).GetBytes(value);
            foreach (var c in data)
            {
                var mt = TextureManager.FontTexture[c];
                spriteBatch.Draw(mt.Texture, position + Vector2.One, mt.Region, Color.Black);
                //spriteBatch.Draw(mt.Texture, position, mt.Region, color);
                position.X += mt.Region.Width;
            }
        }
    }
}
