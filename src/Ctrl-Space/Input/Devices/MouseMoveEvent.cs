using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrl_Space.Input.Devices
{
    delegate void MouseMoveHandler(object sender, MouseMoveEventArgs e);

    class MouseMoveEventArgs
    {
        public MouseMoveEventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }
    }
}
