using System;

namespace Ctrl_Space.Input
{
    delegate void InputDigitalEventHandler(InputDigitalEventArgs e);

    class InputDigitalEventArgs : EventArgs
    {
        private readonly InputDigitalState _state;

        public InputDigitalEventArgs(InputDigitalState state)
        {
            _state = state;
        }

        public InputDigitalState State
        {
            get { return _state; }
        }
    }
}
