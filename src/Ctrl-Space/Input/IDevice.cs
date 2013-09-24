using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ctrl_Space.Input
{
    interface IDevice
    {
        DeviceData[] GetData();
    }
}
