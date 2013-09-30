using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Ctrl_Space.Input.Devices
{
    class XNAGamePad : IDevice
    {
        private List<DeviceData> _deviceData;

        public XNAGamePad(Game game)
        {
            _dataA = new DeviceData() { Device = Name, Event = "A", Data = 1.0f };
            _dataB = new DeviceData() { Device = Name, Event = "B", Data = 1.0f };
            _dataX = new DeviceData() { Device = Name, Event = "X", Data = 1.0f };
            _dataY = new DeviceData() { Device = Name, Event = "Y", Data = 1.0f };
            _dataBack = new DeviceData() { Device = Name, Event = "Back", Data = 1.0f };
            _dataStart = new DeviceData() { Device = Name, Event = "Start", Data = 1.0f };
            _dataBigButton = new DeviceData() { Device = Name, Event = "BigButton", Data = 1.0f };
            _dataLeftShoulder = new DeviceData() { Device = Name, Event = "LeftShoulder", Data = 1.0f };
            _dataRightShoulder = new DeviceData() { Device = Name, Event = "RightShoulder", Data = 1.0f };
            _dataLeftStick = new DeviceData() { Device = Name, Event = "LeftStick", Data = 1.0f };
            _dataRightStick = new DeviceData() { Device = Name, Event = "RightStick", Data = 1.0f };
            _dataUp = new DeviceData() { Device = Name, Event = "Up", Data = 1.0f };
            _dataDown = new DeviceData() { Device = Name, Event = "Down", Data = 1.0f };
            _dataLeft = new DeviceData() { Device = Name, Event = "Left", Data = 1.0f };
            _dataRight = new DeviceData() { Device = Name, Event = "Right", Data = 1.0f };
            _dataLeftX = new DeviceData() { Device = Name, Event = "LeftX", Data = 0.0f };
            _dataLeftY = new DeviceData() { Device = Name, Event = "LeftY", Data = 0.0f };
            _dataRightX = new DeviceData() { Device = Name, Event = "RightX", Data = 0.0f };
            _dataRightY = new DeviceData() { Device = Name, Event = "RightY", Data = 0.0f };
            _dataLeftTrigger = new DeviceData() { Device = Name, Event = "LeftTrigger", Data = 0.0f };
            _dataRightTrigger = new DeviceData() { Device = Name, Event = "RightTrigger", Data = 0.0f };

            _deviceData = new List<DeviceData>();
        }

        public string Name = "XNAGamePad";

        private readonly DeviceData _dataA;
        private readonly DeviceData _dataB;
        private readonly DeviceData _dataX;
        private readonly DeviceData _dataY;
        private readonly DeviceData _dataBack;
        private readonly DeviceData _dataStart;
        private readonly DeviceData _dataBigButton;
        private readonly DeviceData _dataLeftShoulder;
        private readonly DeviceData _dataRightShoulder;
        private readonly DeviceData _dataLeftStick;
        private readonly DeviceData _dataRightStick;
        private readonly DeviceData _dataUp;
        private readonly DeviceData _dataDown;
        private readonly DeviceData _dataLeft;
        private readonly DeviceData _dataRight;
        private readonly DeviceData _dataLeftX;
        private readonly DeviceData _dataLeftY;
        private readonly DeviceData _dataRightX;
        private readonly DeviceData _dataRightY;
        private readonly DeviceData _dataLeftTrigger;
        private readonly DeviceData _dataRightTrigger;

        public IEnumerable<DeviceData> GetData()
        {
            _deviceData.Clear();

            var state = GamePad.GetState(0);
            if (state.Buttons.A == ButtonState.Pressed)
                _deviceData.Add(_dataA);
            if (state.Buttons.B == ButtonState.Pressed)
                _deviceData.Add(_dataB);
            if (state.Buttons.X == ButtonState.Pressed)
                _deviceData.Add(_dataX);
            if (state.Buttons.Y == ButtonState.Pressed)
                _deviceData.Add(_dataY);
            if (state.Buttons.Back == ButtonState.Pressed)
                _deviceData.Add(_dataBack);
            if (state.Buttons.Start == ButtonState.Pressed)
                _deviceData.Add(_dataStart);
            if (state.Buttons.BigButton == ButtonState.Pressed)
                _deviceData.Add(_dataBigButton);
            if (state.Buttons.LeftShoulder == ButtonState.Pressed)
                _deviceData.Add(_dataLeftShoulder);
            if (state.Buttons.RightShoulder == ButtonState.Pressed)
                _deviceData.Add(_dataRightShoulder);
            if (state.Buttons.LeftStick == ButtonState.Pressed)
                _deviceData.Add(_dataLeftStick);
            if (state.Buttons.RightStick == ButtonState.Pressed)
                _deviceData.Add(_dataRightStick);
            if (state.DPad.Up == ButtonState.Pressed)
                _deviceData.Add(_dataUp);
            if (state.DPad.Down == ButtonState.Pressed)
                _deviceData.Add(_dataDown);
            if (state.DPad.Left == ButtonState.Pressed)
                _deviceData.Add(_dataLeft);
            if (state.DPad.Right == ButtonState.Pressed)
                _deviceData.Add(_dataRight);
            if (state.ThumbSticks.Left.X != 0.0f)
            {
                _dataLeftX.Data = state.ThumbSticks.Left.X;
                _deviceData.Add(_dataLeftX);
            }
            if (state.ThumbSticks.Left.Y != 0.0f)
            {
                _dataLeftY.Data = state.ThumbSticks.Left.Y;
                _deviceData.Add(_dataLeftY);
            }
            if (state.ThumbSticks.Right.X != 0.0f)
            {
                _dataRightX.Data = state.ThumbSticks.Right.X;
                _deviceData.Add(_dataRightX);
            }
            if (state.ThumbSticks.Right.Y != 0.0f)
            {
                _dataRightY.Data = state.ThumbSticks.Right.Y;
                _deviceData.Add(_dataRightY);
            }
            if (state.Triggers.Left != 0.0f)
            {
                _dataLeftTrigger.Data = state.Triggers.Left;
                _deviceData.Add(_dataLeftTrigger);
            }
            if (state.Triggers.Right != 0.0f)
            {
                _dataRightTrigger.Data = state.Triggers.Right;
                _deviceData.Add(_dataRightTrigger);
            }
            return _deviceData;
        }
    }
}
