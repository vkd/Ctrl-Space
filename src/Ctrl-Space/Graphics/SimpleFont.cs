using Ctrl_Space.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text;

namespace MonogameTest.Graphics
{
    class SimpleFont
    {
        private readonly Dictionary<char, byte> _symbols;

        public SimpleFont()
        {
            // using ascii for draft
            var encoding = Encoding.GetEncoding(1251);
            _symbols = new Dictionary<char, byte>(256);
            for (int i = 0; i < 256; i++)
            {
                var chr = encoding.GetString(new byte[] { (byte)i })[0];
                if (!_symbols.ContainsKey(chr))
                    _symbols.Add(chr, (byte)i);
            }
        }

        public void DrawText(SpriteBatch spriteBatch, string value, float size, Vector2 position, Color color)
        {
            for (int i = 0; i < value.Length; i++)
            {
                var symbol = _symbols[value[i]];
                DrawSymbol(spriteBatch, symbol, size, position, color);
                position.X += size;
            }
        }

        public void DrawText(SpriteBatch spriteBatch, StringBuilder value, float size, Vector2 position, Color color)
        {
            for (int i = 0; i < value.Length; i++)
            {
                var symbol = _symbols[value[i]];
                DrawSymbol(spriteBatch, symbol, size, position, color);
                position.X += size;
            }
        }

        private void DrawSymbol(SpriteBatch spriteBatch, byte symbol, float size, Vector2 position, Color color)
        {
            var mt = TextureManager.FontTexture[symbol];
            spriteBatch.Draw(mt.Texture, position + Vector2.One, mt.Region, Color.Black, 0f, Vector2.Zero, size / mt.Region.Width, SpriteEffects.None, 0f);
            spriteBatch.Draw(mt.Texture, position, mt.Region, color, 0f, Vector2.Zero, size / mt.Region.Width, SpriteEffects.None, 0f);
        }
    }
}
