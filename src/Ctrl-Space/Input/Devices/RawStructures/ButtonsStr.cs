using System.Runtime.InteropServices;

namespace Ctrl_Space.Input.Devices.RawStructures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ButtonsStr
    {
        [MarshalAs(UnmanagedType.U2)]
        public ushort usButtonFlags;
        [MarshalAs(UnmanagedType.U2)]
        public ushort usButtonData;
    }
}
