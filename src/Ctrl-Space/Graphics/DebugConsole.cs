using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Graphics
{
    class DebugConsole
    {
        private const int _consoleLinesCount = 1024;
        private const int _showLinesCount = 32;
        private readonly StringBuilder[] _strings;
        private int _position;

        public DebugConsole()
        {
            _strings = new StringBuilder[_consoleLinesCount];
            for (int i = 0; i < _consoleLinesCount; i++)
                _strings[i] = new StringBuilder(64);
        }

        public StringBuilder Current
        {
            get { return _strings[_position]; }
        }

        public StringBuilder CurrentLine
        {
            get { var s = _strings[_position]; NewLine(); return s; }
        }

        public void NewLine()
        {
            _position++;
            if (_position >= _consoleLinesCount)
                _position = 0;
            _strings[_position].Clear();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int pos = _position - _showLinesCount;
            if (pos < 0)
                pos += _consoleLinesCount;
            for (int i = 0; i < _showLinesCount; i++)
            {
                spriteBatch.DrawString(TextureManager.Font, _strings[pos], new Vector2(11f, 14f * i + 51f), Color.Black);
                spriteBatch.DrawString(TextureManager.Font, _strings[pos], new Vector2(10f, 14f * i + 50f), Color.Gray);
                pos++;
                if (pos >= _consoleLinesCount)
                    pos = 0;
            }
        }
    }
}
