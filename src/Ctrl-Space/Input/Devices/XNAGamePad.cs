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
                data.Add(new DeviceData() { Device = Name, Event = "A" });
            if (state.Buttons.B == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "B" });
            if (state.Buttons.X == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "X" });
            if (state.Buttons.Y == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Y" });
            if (state.Buttons.Back == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Back" });
            if (state.Buttons.Start == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Start" });
            if (state.Buttons.BigButton == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "BigButton" });
            if (state.Buttons.LeftShoulder == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "LeftShoulder" });
            if (state.Buttons.RightShoulder == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "RightShoulder" });
            if (state.Buttons.LeftStick == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "LeftStick" });
            if (state.Buttons.RightStick == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "RightStick" });
            if (state.DPad.Up == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Up" });
            if (state.DPad.Down == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Down" });
            if (state.DPad.Left == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Left" });
            if (state.DPad.Right == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "Right" });
            if (state.ThumbSticks.Left.X != 0.0f)
                data.Add(new DeviceData() { Device = Name, Event = "LeftX" });
            if (state.ThumbSticks.Left.Y != 0.0f)
                data.Add(new DeviceData() { Device = Name, Event = "LeftY" });
            if (state.ThumbSticks.Right.X != 0.0f)
                data.Add(new DeviceData() { Device = Name, Event = "RightX" });
            if (state.ThumbSticks.Right.Y != 0.0f)
                data.Add(new DeviceData() { Device = Name, Event = "RightY" });
            if (state.Triggers.Left != 0.0f)
                data.Add(new DeviceData() { Device = Name, Event = "LeftTrigger" });
            if (state.Triggers.Right != 0.0f)
                data.Add(new DeviceData() { Device = Name, Event = "RightTrigger" });
            return data.ToArray();
        }
    }
}
