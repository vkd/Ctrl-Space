using System.Collections.Generic;

namespace Ctrl_Space.Input
{
    interface IDevice
    {
        void GetData(List<DeviceData> data);
    }
}
