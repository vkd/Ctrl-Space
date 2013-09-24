using System.Collections.Generic;
using Ctrl_Space.Input.Devices;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.Input
{
    class InputManager
    {
        private bool _isActive = false;

        private Game _game;
        private IDevice[] _devices;
        private ControlMapping[] _controlMapping;

        public InputManager(Game game)
        {
            _game = game;

            _devices = new IDevice[]
            {
                new XNAKeyboard(game),
                new XNAMouse(game),
                new XNAGamePad(game)
            };

            _controlMapping = new ControlMapping[]
            {
                new ControlMapping { Device = "XNAKeyboard", Event = "W", Method = "Up"},
                new ControlMapping { Device = "XNAKeyboard", Event = "S", Method = "Down"},
                new ControlMapping { Device = "XNAKeyboard", Event = "A", Method = "Left"},
                new ControlMapping { Device = "XNAKeyboard", Event = "D", Method = "Right"},
                new ControlMapping { Device = "XNAKeyboard", Event = "Escape", Method = "Exit" },
                new ControlMapping { Device = "XNAMouse", Event = "X", Method = "RotateCW" },
                new ControlMapping { Device = "XNAMouse", Event = "LMB", Method = "PrimaryWeapon" },
                new ControlMapping { Device = "XNAMouse", Event = "RMB", Method = "SecondaryWeapon" }
            };
        }

        public void StartUpdate()
        {
            _isActive = true;
        }

        public void StopUpdate()
        {
            _isActive = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!_isActive)
                return;

            List<DeviceData> data = new List<DeviceData>();
            foreach (IDevice device in _devices)
            {
                data.AddRange(device.GetData());
            }

            var inputState = new InputState();

            foreach (DeviceData d in data)
            {
                foreach (ControlMapping cm in _controlMapping)
                {
                    if (d.Device == cm.Device && d.Event == cm.Event)
                        inputState.Process(cm.Method, d.Data);
                }
            }

            Vector2 move = new Vector2();
            move.X = inputState.Right - inputState.Left;
            move.Y = inputState.Up - inputState.Down;

            if (move.LengthSquared() > 1.0f)
                move.Normalize();

            MoveRightLeft(new InputAnalogEventArgs(move.X));
            MoveUpDown(new InputAnalogEventArgs(move.Y));
            Rotate(new InputAnalogEventArgs(inputState.RotateCW - inputState.RotateCCW));
            PrimaryWeapon(new InputDigitalEventArgs(inputState.PrimaryWeapon ? InputDigitalState.Pressed : InputDigitalState.Released));
            SecondaryWeapon(new InputDigitalEventArgs(inputState.SecondaryWeapon ? InputDigitalState.Pressed : InputDigitalState.Released));
            if (inputState.Exit) ExitGame();
        }

        public event InputAnalogEventHandler MoveUpDown;

        public event InputAnalogEventHandler MoveRightLeft;

        public event InputAnalogEventHandler Rotate;

        public event InputDigitalEventHandler PrimaryWeapon;

        public event InputDigitalEventHandler SecondaryWeapon;

        public event InputPressEventHandler DebugMode;

        public event InputPressEventHandler PlayStopMediaPlayer;

        public event InputPressEventHandler ExitGame;
    }
}
