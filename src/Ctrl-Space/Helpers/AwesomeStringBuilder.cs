using System.Text;

namespace Ctrl_Space.Helpers
{
    class AwesomeStringBuilder
    {
        private readonly StringBuilder _string;

        public AwesomeStringBuilder(int capacity)
        {
            _string = new StringBuilder(capacity);
        }

        public AwesomeStringBuilder Append(string value)
        {
            _string.Append(value);
            return this;
        }

        public AwesomeStringBuilder Append(int value)
        {
            _string.Append(value);
            return this;
        }

        public StringBuilder GetStringBuilder()
        {
            return _string;
        }

        public void Clear()
        {
            _string.Clear();
        }
    }
}
