using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Ctrl_Space.Input.Devices
{
    class XNAGamePad : IDevice
    {
        public XNAGamePad(Game game)
        {

        }

        public string Name = "XNAGamePad";

        public DeviceData[] GetData()
        {
            var data = new List<DeviceData>();
            var state = GamePad.GetState(0);
            if (state.Buttons.A == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "A", Data = 1.0f });
            if (state.Buttons.B == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "B", Data = 1.0f });
            if (state.Buttons.X == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "X", Data = 1.0f });
            if (state.Buttons.Y == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Y", Data = 1.0f });
            if (state.Buttons.Back == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Back", Data = 1.0f });
            if (state.Buttons.Start == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Start", Data = 1.0f });
            if (state.Buttons.BigButton == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "BigButton", Data = 1.0f });
            if (state.Buttons.LeftShoulder == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "LeftShoulder", Data = 1.0f });
            if (state.Buttons.RightShoulder == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "RightShoulder", Data = 1.0f });
            if (state.Buttons.LeftStick == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "LeftStick", Data = 1.0f });
            if (state.Buttons.RightStick == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "RightStick", Data = 1.0f });
            if (state.DPad.Up == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Up", Data = 1.0f });
            if (state.DPad.Down == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Down", Data = 1.0f });
            if (state.DPad.Left == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Left", Data = 1.0f });
            if (state.DPad.Right == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Right", Data = 1.0f });
            if (state.ThumbSticks.Left.X != 0.0f)
                data.Add(new DeviceData() { Device = Name, Event = "LeftX", Data = state.ThumbSticks.Left.X });
            if (state.ThumbSticks.Left.Y != 0.0f)
                data.Add(new DeviceData() { Device = Name, Event = "LeftY", Data = state.ThumbSticks.Left.Y });
            if (state.ThumbSticks.Right.X != 0.0f)
                data.Add(new DeviceData() { Device = Name, Event = "RightX", Data = state.ThumbSticks.Right.X });
            if (state.ThumbSticks.Right.Y != 0.0f)
                data.Add(new DeviceData() { Device = Name, Event = "RightY", Data = state.ThumbSticks.Right.Y });
            if (state.Triggers.Left != 0.0f)
                data.Add(new DeviceData() { Device = Name, Event = "LeftTrigger", Data = state.Triggers.Left });
            if (state.Triggers.Right != 0.0f)
                data.Add(new DeviceData() { Device = Name, Event = "RightTrigger", Data = state.Triggers.Right });
            return data.ToArray();
        }
    }
}
