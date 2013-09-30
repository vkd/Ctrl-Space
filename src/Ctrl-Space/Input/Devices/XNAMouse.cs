using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ctrl_Space.Input.Devices
{
    class XNAMouse : IDevice
    {
        private readonly List<DeviceData> _deviceData;
        private Point _center;

        public XNAMouse(Game game)
        {
            _dataLMB = new DeviceData() { Device = Name, Event = "LMB", Data = 1.0f };
            _dataMMB = new DeviceData() { Device = Name, Event = "MMB", Data = 1.0f };
            _dataRMB = new DeviceData() { Device = Name, Event = "RMB", Data = 1.0f };
            _dataX = new DeviceData() { Device = Name, Event = "X", Data = 0.0f };
            _dataY = new DeviceData() { Device = Name, Event = "Y", Data = 0.0f };

            _deviceData = new List<DeviceData>();
            _center = new Point(game._graphics.GraphicsDevice.Viewport.Width / 2, game._graphics.GraphicsDevice.Viewport.Height / 2);
            game.IsMouseVisible = false;
            Mouse.SetPosition(_center.X, _center.Y);
        }

        public readonly string Name = "XNAMouse";

        private readonly DeviceData _dataLMB;
        private readonly DeviceData _dataMMB;
        private readonly DeviceData _dataRMB;
        private readonly DeviceData _dataX;
        private readonly DeviceData _dataY;

        public IEnumerable<DeviceData> GetData()
        {
            _deviceData.Clear();

            var state = Mouse.GetState();
            Mouse.SetPosition(_center.X, _center.Y);

            if (state.LeftButton == ButtonState.Pressed)
                _deviceData.Add(_dataLMB);
            if (state.MiddleButton == ButtonState.Pressed)
                _deviceData.Add(_dataMMB);
            if (state.RightButton == ButtonState.Pressed)
                _deviceData.Add(_dataRMB);
            if (state.X - _center.X != 0)
            {
                _dataX.Data = (state.X - _center.X) * .02f;
                _deviceData.Add(_dataX);
            }
            if (state.Y - _center.Y != 0)
            {
                _dataY.Data = (state.Y - _center.Y) * .02f;
                _deviceData.Add(_dataY);
            }
            return _deviceData;
        }
    }
}
