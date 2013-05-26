using System;
using System.Runtime.InteropServices;
using Ctrl_Space.Input.Devices.RawStructures;

namespace Ctrl_Space.Input.Devices
{
    static class RawInputMouse
    {
        delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        const int GWL_WNDPROC = -4;
        const int WM_INPUT = 0x00FF;
        const int RID_INPUT = 0x10000003;
        const int RIM_TYPEMOUSE = 0; 

        static bool _isInitialized;
        static IntPtr prevWndProc;
        static WndProc hookProcDelegate;
        //static IntPtr hIMC;

        [DllImport("user32.dll")]
        static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, 
            IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int GetRawInputData(IntPtr hRawInput, int uiCommand,
            out RawInput pData, ref int pcbSize, int cbSizeHeader);

        // This static method is required because legacy OSes do not support
        // SetWindowLongPtr
        public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll")]
        public static extern bool RegisterRawInputDevices(
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] RawInputDevice[] pRawInputDevices,
            int uiNumDevices, int cbSize);

        public static bool Initialize(IntPtr handle)
        {
            if (_isInitialized)
            {
                //throw new InvalidOperationException("TextInput.Initialize can only be called once!");
                return false;
            }
            hookProcDelegate = new WndProc(HookProc);
            prevWndProc = SetWindowLongPtr(new HandleRef(null, handle), GWL_WNDPROC,
                Marshal.GetFunctionPointerForDelegate(hookProcDelegate));
            //hIMC = ImmGetContext(handle);

            RawInputDevice[] rid = new RawInputDevice[1];
            rid[0].UsagePage = HIDUsagePage.Generic;    //0x01;
            rid[0].Usage = HIDUsage.Mouse;              //0x02;
            rid[0].Flags = RawInputDeviceFlags.InputSink;
            rid[0].WindowHandle = handle;

            if (!RegisterRawInputDevices(rid, rid.Length, Marshal.SizeOf(rid[0])))
            {
                //throw new ApplicationException("Failed to register raw input device(s).");
                return false;
            }

            _isInitialized = true;
            return true;
        }

        private static IntPtr HookProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            IntPtr returnCode = CallWindowProc(prevWndProc, hWnd, msg, wParam, lParam);
            switch (msg)
            {
                case WM_INPUT:
                    RawInput input;
                    int dwSize = Marshal.SizeOf(typeof(RawInput));
    
                    int outSize = GetRawInputData(lParam, RID_INPUT, 
                                    out input, ref dwSize, 
                                    Marshal.SizeOf(typeof(RawInputHeader)));
    
                    if (outSize != -1)
                    {
                        if (input.Header.Type == RIM_TYPEMOUSE) 
                        {
                            int x = input.Data.Mouse.lLastX;
                            int y = input.Data.Mouse.lLastY;
                            MouseMove(null, new MouseMoveEventArgs(x, y));
                        }
                    }
                    break;
            } 
            return returnCode;
        }

        public static event MouseMoveHandler MouseMove;
    }
}
