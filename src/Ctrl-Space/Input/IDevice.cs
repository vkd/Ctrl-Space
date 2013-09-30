using System.Collections.Generic;

namespace Ctrl_Space.Input
{
    interface IDevice
    {
        IEnumerable<DeviceData> GetData();
    }
}
