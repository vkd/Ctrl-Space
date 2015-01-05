using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ctrl_Space.Input.Devices
{
    class XNAMouse : IDevice
    {
        public readonly bool FreezeOnCenter = true;

        private Point _center;
        private Point _lastPosition;

        public XNAMouse(Game game)
        {
            _dataLMB = new DeviceData() { Device = Name, Event = "LMB", Data = 1.0f };
            _dataMMB = new DeviceData() { Device = Name, Event = "MMB", Data = 1.0f };
            _dataRMB = new DeviceData() { Device = Name, Event = "RMB", Data = 1.0f };
            _dataX = new DeviceData() { Device = Name, Event = "X", Data = 0.0f };
            _dataY = new DeviceData() { Device = Name, Event = "Y", Data = 0.0f };

            _center = new Point(game._graphics.GraphicsDevice.Viewport.Width / 2, game._graphics.GraphicsDevice.Viewport.Height / 2);
            if (FreezeOnCenter)
            {
                Mouse.SetPosition(_center.X, _center.Y);
            }
            _lastPosition = Mouse.GetState().Position;
        }

        public readonly string Name = "XNAMouse";

        private readonly DeviceData _dataLMB;
        private readonly DeviceData _dataMMB;
        private readonly DeviceData _dataRMB;
        private readonly DeviceData _dataX;
        private readonly DeviceData _dataY;

        public void GetData(List<DeviceData> data)
        {
            var state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed)
                data.Add(_dataLMB);
            if (state.MiddleButton == ButtonState.Pressed)
                data.Add(_dataMMB);
            if (state.RightButton == ButtonState.Pressed)
                data.Add(_dataRMB);
            if (state.X - _lastPosition.X != 0)
            {
                _dataX.Data = (state.X - _lastPosition.X) * .02f;
                data.Add(_dataX);
            }
            if (state.Y - _lastPosition.Y != 0)
            {
                _dataY.Data = (state.Y - _lastPosition.Y) * .02f;
                data.Add(_dataY);
            }

            if (FreezeOnCenter)
            {
                Mouse.SetPosition(_center.X, _center.Y);
            }
            _lastPosition = Mouse.GetState().Position;
        }
    }
}
