using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Ctrl_Space.Input.Devices
{
    class XNAKeyboard : IDevice
    {
        private List<DeviceData> _deviceData;

        public XNAKeyboard(Game game)
        {
            _deviceData = new List<DeviceData>();
        }

        public string Name = "XNAKeyboard";

        public IEnumerable<DeviceData> GetData()
        {
            _deviceData.Clear();

            var state = Keyboard.GetState();
            var keys = state.GetPressedKeys();
            foreach (var key in keys)
                _deviceData.Add(new DeviceData() { Device = Name, Event = key.ToString(), Data = 1.0f });
            return _deviceData;
        }
    }
}
