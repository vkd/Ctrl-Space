using Ctrl_Space.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonogameTest.Graphics;
using System.Text;

namespace Ctrl_Space.Graphics
{
    class DebugConsole
    {
        private const int _consoleLinesCount = 1024;
        private const int _showLinesCount = 32;
        private readonly StringBuilder[] _strings;
        private int _position;
        private readonly AwesomeStringBuilder _currentString;

        public DebugConsole()
        {
            _currentString = new AwesomeStringBuilder(1024);
            _strings = new StringBuilder[_consoleLinesCount];
            for (int i = 0; i < _consoleLinesCount; i++)
                _strings[i] = new StringBuilder(64);
        }

        public DebugConsole Append(string value)
        {
            _currentString.Append(value);
            return this;
        }

        public DebugConsole AppendLine(string value)
        {
            Append(value).NewLine();
            return this;
        }

        public DebugConsole Append(int value)
        {
            _currentString.Append(value);
            return this;
        }

        public DebugConsole NewLine()
        {
            _strings[_position].Clear();
            _currentString.AppendToStringBuilder(_strings[_position]);
            _currentString.Clear();
            _position++;
            if (_position >= _consoleLinesCount)
                _position = 0;
            return this;
        }

        public void Draw(SpriteBatch spriteBatch, SimpleFont font)
        {
            int pos = _position - _showLinesCount;
            if (pos < 0)
                pos += _consoleLinesCount;
            for (int i = 0; i < _showLinesCount; i++)
            {
                font.DrawText(spriteBatch, _strings[pos], 14f, new Vector2(10f, 14f * i + 50f), Color.Gray);
                pos++;
                if (pos >= _consoleLinesCount)
                    pos = 0;
            }
        }
    }
}
