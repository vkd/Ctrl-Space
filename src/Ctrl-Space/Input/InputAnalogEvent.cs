using System;

namespace Ctrl_Space.Input
{
    delegate void InputAnalogEventHandler(InputAnalogEventArgs e);

    class InputAnalogEventArgs : EventArgs
    {
        private readonly float _value;

        public InputAnalogEventArgs(float value)
        {
            _value = value;
        }

        public float Value
        {
            get { return _value; }
        }
    }
}
