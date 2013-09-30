using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Ctrl_Space.Input.Devices
{
    class XNAKeyboard : IDevice
    {
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
        }

        public readonly string Name = "XNAKeyboard";

        public void GetData(List<DeviceData> data)
        {
            var state = Keyboard.GetState();
            var keys = state.GetPressedKeys();
            for (int i = 0; i < _keys.Length; i++)
                if (state.IsKeyDown(_keys[i]))
                    data.Add(_datas[i]);
        }
    }
}
