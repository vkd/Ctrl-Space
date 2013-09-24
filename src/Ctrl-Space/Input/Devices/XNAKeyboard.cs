using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Ctrl_Space.Input.Devices
{
    class XNAKeyboard : IDevice
    {
        public XNAKeyboard(Game game)
        {

        }

        public string Name = "XNAKeyboard";

        public DeviceData[] GetData()
        {
            var data = new List<DeviceData>();

            var state = Keyboard.GetState();
            var keys = state.GetPressedKeys();
            foreach (var key in keys)
                data.Add(new DeviceData() { Device = Name, Event = key.ToString(), Data = 1.0f });
            return data.ToArray();
        }
    }
}
