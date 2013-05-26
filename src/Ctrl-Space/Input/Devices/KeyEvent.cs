using System;

namespace Ctrl_Space.Input.Devices
{
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);

    public class KeyEventArgs : EventArgs
    {
        private char keyCode;

        public KeyEventArgs(char keyCode)
        {
            this.keyCode = keyCode;
        }

        public char KeyCode
        {
            get { return keyCode; }
        }
    }
}
