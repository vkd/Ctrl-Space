using System.Text;

namespace Ctrl_Space.Helpers
{
    class AwesomeStringBuilder
    {
        private readonly char[] _string;
        private int _position;
        private readonly int _capacity;
        private readonly char[] _digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public AwesomeStringBuilder(int capacity)
        {
            _capacity = capacity;
            _position = 0;
            _string = new char[capacity];
        }

        public AwesomeStringBuilder Append(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (_position >= _capacity)
                    break;
                _string[_position] = value[i];
                _position++;         
            }
            return this;
        }

        public AwesomeStringBuilder Append(int value)
        {
            bool _negative = value < 0;
            if (_negative)
                value = -value;
            int begin = _position;
            do
            {
                if (_position >= _capacity)
                    break;
                _string[_position++] = _digits[value % 10];
                value /= 10;
            }
            while (value != 0);
            if (_position < _capacity && _negative)
                _string[_position++] = '-';
            int end = _position;
            while (begin < end)
            {
                char t = _string[begin];
                _string[begin++] = _string[--end];
                _string[end] = t;
            }
            return this;
        }

        public void AppendToStringBuilder(StringBuilder stringBuilder)
        {
            stringBuilder.Append(_string, 0, _position);
        }

        public void Clear()
        {
            _position = 0;
        }
    }
}
