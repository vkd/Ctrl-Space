using System.Runtime.InteropServices;

namespace Ctrl_Space.Input.Devices.RawStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RawHID
    {
        [MarshalAs(UnmanagedType.U4)]
        public int dwSizHid;
        [MarshalAs(UnmanagedType.U4)]
        public int dwCount;
    }
}
