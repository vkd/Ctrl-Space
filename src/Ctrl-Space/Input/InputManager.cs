using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ctrl_Space.Input
{
    class InputManager : IInputManager
    {
        private bool _isActive = false;

        //Keyboard
        private KeyboardState _keyboardState;
        private KeyboardState _oldKeyboardState;

        //GamePad
        private GamePadState _gamePadState;
        private GamePadState _oldGamePadState;

        //Mouse
        private MouseState _mouseState;
        private MouseState _oldMouseState;
        private Point _mouseCenterPosition;

        private Game _game;

        public InputManager(Game game)
        {
            _game = game;

            _mouseCenterPosition = new Point(_game._graphics.GraphicsDevice.Viewport.Width / 2,
                _game._graphics.GraphicsDevice.Viewport.Height / 2);

            _oldMouseState = new MouseState();
            _game.IsMouseVisible = false;
        }

        public void StartUpdate()
        {
            SetPosition(_mouseCenterPosition);
            _isActive = true;
        }

        public void StopUpdate()
        {
            _isActive = false;
        }

        public void Update(GameTime gameTime)
        {
            UpdateMouse(gameTime);
            UpdateGamePad(gameTime);
            UpdateKeyboard(gameTime);  
        }

        private void UpdateMouse(GameTime gameTime)
        {
            if (!_isActive)
                return;

            _mouseState = Mouse.GetState();

            if (_mouseState.LeftButton == ButtonState.Pressed 
                && _oldMouseState.LeftButton == ButtonState.Released)
            {
                PrimaryWeapon(new InputDigitalEventArgs(InputDigitalState.Pressed));
            }
            if (_mouseState.LeftButton == ButtonState.Released
                && _oldMouseState.LeftButton == ButtonState.Pressed)
            {
                PrimaryWeapon(new InputDigitalEventArgs(InputDigitalState.Released));
            }

            if (_mouseState.RightButton == ButtonState.Pressed
                && _oldMouseState.RightButton == ButtonState.Released)
            {
                SecondaryWeapon(new InputDigitalEventArgs(InputDigitalState.Pressed));
            }
            if (_mouseState.RightButton == ButtonState.Released
                && _oldMouseState.RightButton == ButtonState.Pressed)
            {
                SecondaryWeapon(new InputDigitalEventArgs(InputDigitalState.Released));
            }

            Rotate(new InputAnalogEventArgs((_mouseState.X - _mouseCenterPosition.X) * 0.02f));

            SetPosition(_mouseCenterPosition);
            _oldMouseState = _mouseState;
        }

        private void SetPosition(Point position)
        {
            Mouse.SetPosition(position.X, position.Y);
        }

        private void UpdateGamePad(GameTime gameTime)
        {
            _gamePadState = GamePad.GetState(0);

            if (IsButtonDown(Buttons.Back))
                ExitGame();

            if (IsButtonDown(Buttons.DPadRight))
                MoveRightLeft(new InputAnalogEventArgs(1.0f));
            if (IsButtonDown(Buttons.DPadLeft))
                MoveRightLeft(new InputAnalogEventArgs(-1.0f));

            if (IsButtonDown(Buttons.DPadUp))
                MoveUpDown(new InputAnalogEventArgs(1.0f));
            else if (IsButtonDown(Buttons.DPadDown))
                MoveUpDown(new InputAnalogEventArgs(-1.0f));

            MoveUpDown(new InputAnalogEventArgs(_gamePadState.ThumbSticks.Left.Y));
            MoveRightLeft(new InputAnalogEventArgs(_gamePadState.ThumbSticks.Left.X));

            if (ButtonsPressed(Buttons.A))
            {
                PrimaryWeapon(new InputDigitalEventArgs(InputDigitalState.Pressed));
            }
            if (ButtonsReleased(Buttons.A))
            {
                PrimaryWeapon(new InputDigitalEventArgs(InputDigitalState.Released));
            }

            if (ButtonsPressed(Buttons.B))
            {
                SecondaryWeapon(new InputDigitalEventArgs(InputDigitalState.Pressed));
            }
            if (ButtonsReleased(Buttons.B))
            {
                SecondaryWeapon(new InputDigitalEventArgs(InputDigitalState.Released));
            }

            if (ButtonsPressed(Buttons.RightShoulder))
            {
                PrimaryWeapon(new InputDigitalEventArgs(InputDigitalState.Pressed));
            }
            if (ButtonsReleased(Buttons.RightShoulder))
            {
                PrimaryWeapon(new InputDigitalEventArgs(InputDigitalState.Released));
            }

            if (ButtonsPressed(Buttons.LeftShoulder))
            {
                SecondaryWeapon(new InputDigitalEventArgs(InputDigitalState.Pressed));
            }
            if (ButtonsReleased(Buttons.LeftShoulder))
            {
                SecondaryWeapon(new InputDigitalEventArgs(InputDigitalState.Released));
            }

            Rotate(new InputAnalogEventArgs(_gamePadState.ThumbSticks.Right.X));

            _oldGamePadState = _gamePadState;
        }

        private bool ButtonsPressed(Buttons buttons)
        {
            return _gamePadState.IsButtonDown(buttons) && _oldGamePadState.IsButtonUp(buttons);
        }

        private bool ButtonsReleased(Buttons buttons)
        {
            return _gamePadState.IsButtonUp(buttons) && _oldGamePadState.IsButtonDown(buttons);
        }

        private bool IsButtonDown(Buttons buttons)
        {
            return _gamePadState.IsButtonDown(buttons);
        }

        private void UpdateKeyboard(GameTime gameTime)
        {
            if (!_isActive)
                return;

            _keyboardState = Keyboard.GetState();

            if (KeysPressed(Keys.Escape))
                ExitGame();

            if (KeysPressed(Keys.F1))
                DebugMode();

            if (KeysPressed(Keys.Tab))
                PlayStopMediaPlayer();

            if (IsKeyDown(Keys.D))
                MoveRightLeft(new InputAnalogEventArgs(1.0f));
            if (IsKeyDown(Keys.A))
                MoveRightLeft(new InputAnalogEventArgs(-1.0f));

            if (IsKeyDown(Keys.W))
                MoveUpDown(new InputAnalogEventArgs(1.0f));
            if (IsKeyDown(Keys.S))
                MoveUpDown(new InputAnalogEventArgs(-1.0f));

            //if (IsKeyDown(Keys.D) || IsKeyDown(Keys.Right))
            //    TryActionFloat(InputActionFloatType.MoveRightLeft, SensitivityKeyboard);
            //else if (IsKeyDown(Keys.A) || IsKeyDown(Keys.Left))
            //    TryActionFloat(InputActionFloatType.MoveRightLeft, -SensitivityKeyboard);

            //if (IsKeyDown(Keys.W) || IsKeyDown(Keys.Up))
            //    TryActionFloat(InputActionFloatType.MoveUpDown, SensitivityKeyboard);
            //else if (IsKeyDown(Keys.S) || IsKeyDown(Keys.Down))
            //    TryActionFloat(InputActionFloatType.MoveUpDown, -SensitivityKeyboard);

            if (KeysPressed(Keys.Space))
                PrimaryWeapon(new InputDigitalEventArgs(InputDigitalState.Pressed));
            if (KeysReleased(Keys.Space))
                PrimaryWeapon(new InputDigitalEventArgs(InputDigitalState.Released));

            if (KeysPressed(Keys.LeftShift))
                SecondaryWeapon(new InputDigitalEventArgs(InputDigitalState.Pressed));
            if (KeysReleased(Keys.LeftShift))
                SecondaryWeapon(new InputDigitalEventArgs(InputDigitalState.Released));

            _oldKeyboardState = _keyboardState;
        }

        private bool KeysPressed(Keys keys)
        {
            return _keyboardState.IsKeyDown(keys) && _oldKeyboardState.IsKeyUp(keys);
        }

        private bool KeysReleased(Keys keys)
        {
            return _keyboardState.IsKeyUp(keys) && _oldKeyboardState.IsKeyDown(keys);
        }

        private bool IsKeyDown(Keys keys)
        {
            return _keyboardState.IsKeyDown(keys);
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
