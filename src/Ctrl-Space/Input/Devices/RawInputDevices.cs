using System;
using System.Runtime.InteropServices;
using Ctrl_Space.Input.Devices.RawStructures;

namespace Ctrl_Space.Input.Devices
{
    static class RawInputDevices
    {
        delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        const int GWL_WNDPROC = -4;

        const int WM_INPUT = 0x00FF;
        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;
        const int WM_CHAR = 0x102;
        const int WM_IME_SETCONTEXT = 0x0281;
        const int WM_INPUTLANGCHANGE = 0x51;
        const int WM_GETDLGCODE = 0x87;
        const int DLGC_WANTALLKEYS = 4;

        const int RID_INPUT = 0x10000003;
        const int RIM_TYPEMOUSE = 0; 

        static bool _isInitialized;
        static IntPtr prevWndProc;
        static WndProc hookProcDelegate;
        static IntPtr hIMC;

        [DllImport("Imm32.dll")]
        static extern IntPtr ImmGetContext(IntPtr hWnd);
        [DllImport("Imm32.dll")]
        static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);

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
            hIMC = ImmGetContext(handle);

            RawInputDevice[] rid = new RawInputDevice[1];
            rid[0].UsagePage = HIDUsagePage.Generic;    //0x01;
            rid[0].Usage = HIDUsage.Mouse;              //0x02;
            rid[0].Flags = RawInputDeviceFlags.InputSink;// | RawInputDeviceFlags.CaptureMouse;
            rid[0].WindowHandle = handle;

            //rid[1].UsagePage = HIDUsagePage.Keyboard;   
            //rid[1].Usage = HIDUsage.Keyboard;   
            //rid[1].Flags = RawInputDeviceFlags.AppKeys;
            //rid[1].WindowHandle = handle;

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
                case WM_GETDLGCODE:
                    returnCode = (IntPtr)(returnCode.ToInt32() | DLGC_WANTALLKEYS);
                    break;

                case WM_KEYDOWN:
                    if (KeyDown != null)
                        KeyDown(null, new KeyEventArgs((char)wParam));
                    break;

                case WM_KEYUP:
                    if (KeyUp != null)
                        KeyUp(null, new KeyEventArgs((char)wParam));
                    break;

                case WM_CHAR:
                    if (CharEntered != null)
                        CharEntered(null, new CharacterEventArgs((char)wParam, lParam.ToInt32()));
                    break;

                case WM_IME_SETCONTEXT:
                    if (wParam.ToInt32() == 1)
                        ImmAssociateContext(hWnd, hIMC);
                    break;

                case WM_INPUTLANGCHANGE:
                    ImmAssociateContext(hWnd, hIMC);
                    returnCode = (IntPtr)1;
                    break;
            } 
            return returnCode;
        }

        /// <summary>
        /// Event raised when a mouse has been move
        /// </summary>
        public static event MouseMoveHandler MouseMove;

        /// <summary>
        /// Event raised when a character has been entered.
        /// </summary>
        public static event CharEnteredHandler CharEntered;

        /// <summary>
        /// Event raised when a key has been pressed down. May fire multiple times due to keyboard repeat.
        /// </summary>
        public static event KeyEventHandler KeyDown;

        /// <summary>
        /// Event raised when a key has been released.
        /// </summary>
        public static event KeyEventHandler KeyUp;
    }
}
