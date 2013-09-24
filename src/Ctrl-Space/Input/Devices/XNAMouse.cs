using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ctrl_Space.Input.Devices
{
    class XNAMouse : IDevice
    {
        private Point _center;

        public XNAMouse(Game game)
        {
            _center = new Point(game._graphics.GraphicsDevice.Viewport.Width / 2,
                game._graphics.GraphicsDevice.Viewport.Height / 2);

            game.IsMouseVisible = false;

            Mouse.SetPosition(_center.X, _center.Y);
        }

        public string Name = "XNAMouse";

        public DeviceData[] GetData()
        {
            var data = new List<DeviceData>();

            var state = Mouse.GetState();
            Mouse.SetPosition(_center.X, _center.Y);

            if (state.LeftButton == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "LMB", Data = 1.0f });
            if (state.MiddleButton == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "MMB", Data = 1.0f });
            if (state.RightButton == ButtonState.Pressed)
                data.Add(new DeviceData() { Device = Name, Event = "RMB", Data = 1.0f });
            if (state.X - _center.X != 0)
                data.Add(new DeviceData() { Device = Name, Event = "X", Data = (state.X - _center.X) * .02f });
            if (state.Y - _center.Y != 0)
                data.Add(new DeviceData() { Device = Name, Event = "Y", Data = (state.Y - _center.Y) * .02f });
            return data.ToArray();
        }
    }
}
