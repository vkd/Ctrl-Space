using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Ctrl_Space.Input.Devices
{
    class XNAKeyboard : IDevice
    {
        private readonly List<DeviceData> _deviceData;
        private readonly Keys[] _keys;
        private readonly DeviceData[] _datas;

        public XNAKeyboard(Game game)
        {
            var allKeys = Enum.GetValues(typeof(Keys));
            _keys = new Keys[allKeys.Length];
            _datas = new DeviceData[allKeys.Length];
            for (int k = 0; k < allKeys.Length; k++)
            {
                _keys[k] = (Keys)allKeys.GetValue(k);
                _datas[k] = new DeviceData() { Device = Name, Event = Enum.GetName(typeof(Keys), _keys[k]), Data = 1.0f };
            }
            _deviceData = new List<DeviceData>();
        }

        public readonly string Name = "XNAKeyboard";

        public IEnumerable<DeviceData> GetData()
        {
            _deviceData.Clear();

            var state = Keyboard.GetState();
            var keys = state.GetPressedKeys();
            for (int i = 0; i < _keys.Length; i++)
                if (state.IsKeyDown(_keys[i]))
                    _deviceData.Add(_datas[i]);
            return _deviceData;
        }
    }
}
