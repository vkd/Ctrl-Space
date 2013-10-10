using System.Text;

namespace Ctrl_Space.Helpers
{
    class AwesomeStringBuilder
    {
        private readonly char[] _string;
        private int _position;
        private readonly int _capacity;

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

        //public AwesomeStringBuilder Append(int value)
        //{
        //    _string.Append(value);
        //    return this;
        //}

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
