using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Ctrl_Space
{
    delegate void InputActionDelegate(); 
    delegate void InputActionDelegateFloat(float floatParam);

    class Archive_InputManager
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

        private Dictionary<InputActionType, InputActionDelegate> _actions = 
            new Dictionary<InputActionType, InputActionDelegate>();

        private Dictionary<InputActionFloatType, InputActionDelegateFloat> _actionsFloat =
            new Dictionary<InputActionFloatType, InputActionDelegateFloat>();

        private Game _game;

        public Archive_InputManager(Game game)
        {
            _game = game;
        }

        public void Initialize()
        {
            _isActive = true;

            SensitivityMouse = 0.02f;
            SensitivityThumbSticks = 1.0f;
            SensitivityKeyboard = 1.0f;

            _mouseCenterPosition = new Point(_game._graphics.GraphicsDevice.Viewport.Width / 2,
                _game._graphics.GraphicsDevice.Viewport.Height / 2);

            _oldMouseState = new MouseState();
            _game.IsMouseVisible = false;
        }

        public void AddAction(InputActionType inputActionType, InputActionDelegate inputAction)
        {
            _actions.Add(inputActionType, inputAction);
        }

        public void AddActionFloat(InputActionFloatType inputActionFloatType, InputActionDelegateFloat inputMouseDelegate)
        {
            _actionsFloat.Add(inputActionFloatType, inputMouseDelegate);
        }

        public void StartUpdate()
        {
            _isActive = true;
        }

        public void StopUpdate()
        {
            _isActive = false;
        }

        private void TryAction(InputActionType inputActionType)
        {
            InputActionDelegate del;
            if (_actions.TryGetValue(inputActionType, out del))
                del();
        }

        private void TryActionFloat(InputActionFloatType inputActionFloatType, float param)
        {
            InputActionDelegateFloat del;
            if (_actionsFloat.TryGetValue(inputActionFloatType, out del))
                del(param);
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

            if (_mouseState.LeftButton == ButtonState.Pressed &&
                _oldMouseState.LeftButton == ButtonState.Released)
            {
                TryAction(InputActionType.Strike);
            }

            if (_mouseState.RightButton == ButtonState.Pressed &&
                _oldMouseState.RightButton == ButtonState.Released)
            {
                TryAction(InputActionType.Rocket);
            }

            TryActionFloat(InputActionFloatType.Rotate, 
                (_mouseState.X - _mouseCenterPosition.X) * SensitivityMouse);

            Mouse.SetPosition(_mouseCenterPosition.X, _mouseCenterPosition.Y);
            _oldMouseState = _mouseState;
        }

        private void UpdateGamePad(GameTime gameTime)
        {
            _gamePadState = GamePad.GetState(0);

            if (IsButtonDown(Buttons.Back))
                TryAction(InputActionType.ExitGame);

            if (IsButtonDown(Buttons.DPadRight))
                TryActionFloat(InputActionFloatType.MoveRightLeft, SensitivityKeyboard);
            else if (IsButtonDown(Buttons.DPadLeft))
                TryActionFloat(InputActionFloatType.MoveRightLeft, -SensitivityKeyboard);

            if (IsButtonDown(Buttons.DPadUp))
                TryActionFloat(InputActionFloatType.MoveUpDown, SensitivityKeyboard);
            else if (IsButtonDown(Buttons.DPadDown))
                TryActionFloat(InputActionFloatType.MoveUpDown, -SensitivityKeyboard);

            TryActionFloat(InputActionFloatType.MoveUpDown,
                _gamePadState.ThumbSticks.Left.Y * SensitivityThumbSticks);
            TryActionFloat(InputActionFloatType.MoveRightLeft,
                _gamePadState.ThumbSticks.Left.X * SensitivityThumbSticks);

            if (IsButtonDown(Buttons.A))
                TryAction(InputActionType.Strike);
            if (IsButtonDown(Buttons.B))
                TryAction(InputActionType.Rocket);

            if (IsButtonDown(Buttons.RightShoulder))
                TryAction(InputActionType.Strike);
            if (IsButtonDown(Buttons.LeftShoulder))
                TryAction(InputActionType.Rocket);

            TryActionFloat(InputActionFloatType.Rotate,
                _gamePadState.ThumbSticks.Right.X * SensitivityThumbSticks);
            
            _oldGamePadState = _gamePadState;
        }

        private bool ButtonsPressed(Buttons buttons)
        {
            return _gamePadState.IsButtonDown(buttons) && _oldGamePadState.IsButtonUp(buttons);
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
                TryAction(InputActionType.ExitGame);

            if (KeysPressed(Keys.F1))
                TryAction(InputActionType.DebugMode);

            if (KeysPressed(Keys.Tab))
                TryAction(InputActionType.PlayStopMediaPlayer);

            if (IsKeyDown(Keys.D) || IsKeyDown(Keys.Right))
                TryActionFloat(InputActionFloatType.MoveRightLeft, SensitivityKeyboard);
            else if (IsKeyDown(Keys.A) || IsKeyDown(Keys.Left))
                TryActionFloat(InputActionFloatType.MoveRightLeft, -SensitivityKeyboard);

            if (IsKeyDown(Keys.W) || IsKeyDown(Keys.Up))
                TryActionFloat(InputActionFloatType.MoveUpDown, SensitivityKeyboard);
            else if (IsKeyDown(Keys.S) || IsKeyDown(Keys.Down))
                TryActionFloat(InputActionFloatType.MoveUpDown, -SensitivityKeyboard);

            if (KeysPressed(Keys.Space))
                TryAction(InputActionType.Strike);

            if (KeysPressed(Keys.LeftShift))
                TryAction(InputActionType.Rocket);

            _oldKeyboardState = _keyboardState;
        }

        private bool KeysPressed(Keys keys)
        {
            return _keyboardState.IsKeyDown(keys) && _oldKeyboardState.IsKeyUp(keys);
        }

        private bool IsKeyDown(Keys keys)
        {
            return _keyboardState.IsKeyDown(keys);
        }

        public float SensitivityMouse
        {
            get;
            set;
        }

        public float SensitivityThumbSticks
        {
            get;
            set;
        }

        public float SensitivityKeyboard
        {
            get;
            set;
        }
    }

    enum InputActionType
    {
        ExitGame,
        DebugMode,
        PlayStopMediaPlayer,
        Rocket,
        Strike
    }

    enum InputActionFloatType
    {
        MoveRightLeft,
        MoveUpDown,
        Rotate
    }
}
