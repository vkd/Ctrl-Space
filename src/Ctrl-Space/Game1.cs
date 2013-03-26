using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Ctrl_Space
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        Texture2D _myFirstTexture;
        Texture2D _meteoriteTexture;
        Texture2D _rocketTexture;
        Texture2D _speedBonusTexture;

        //Keyboard
        private KeyboardState _keyboardState;
        private KeyboardState _oldKeyboardState;

        //GamePad
        private GamePadState _gamePadState;
        private GamePadState _oldGamePadState;

        Ship _ship;

        Camera _camera;
        
        List<GameObject> _asteroids = new List<GameObject>();
        List<SpeedBonus> _speedBonuses = new List<SpeedBonus>();
        List<RocketWeapon> _rockets = new List<RocketWeapon>();

        private TexturesManager _textureManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
        }

        protected override void Initialize()
        {
            Random r = new Random();

            int maxWidth = GraphicsDevice.Viewport.Width;
            int maxHeight = GraphicsDevice.Viewport.Height;

            _ship = new Ship(new Vector2(maxWidth / 2, maxHeight / 2));
            _camera = new Camera(_ship);

            for (int i = 0; i < 10; ++i)
            {
                GameObject asteroid = new GameObject(
                    (float)(r.NextDouble() * 40 + 20),
                    new Vector2((float)(r.NextDouble() * maxWidth), (float)(r.NextDouble() * maxHeight)),
                    new Vector2((float)(r.NextDouble() * 4 - 2), (float)(r.NextDouble() * 4 - 2)),
                    (float)(r.NextDouble() * 6.28),
                    (float)(r.NextDouble() * .1 - .05));

                _asteroids.Add(asteroid);
            }

            for (int i = 0; i < 5; ++i)
            {
                SpeedBonus bonus = new SpeedBonus(new Vector2((float)(r.NextDouble() * maxWidth), (float)(r.NextDouble() * maxHeight)));
                _speedBonuses.Add(bonus);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureManager = new TexturesManager(Content);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            InputDeviceUpdate(gameTime);
            
            _ship.Position += _ship.Speed;
            
            _ship.Speed *= 0.99f;

            _ship.Position.X = (_ship.Position.X + GraphicsDevice.Viewport.Width) % GraphicsDevice.Viewport.Width;
            _ship.Position.Y = (_ship.Position.Y + GraphicsDevice.Viewport.Height) % GraphicsDevice.Viewport.Height;
            _ship.BB.Min = new Vector3(_ship.Position.X - _ship.Size / 2, _ship.Position.Y - _ship.Size / 2, 0);
            _ship.BB.Max = new Vector3(_ship.Position.X - _ship.Size / 2, _ship.Position.Y + _ship.Size / 2, 0);

            for (int i = 0; i < _asteroids.Count; ++i)
            {
                bool asteroidDestroyed = false;
                _asteroids[i].Position += _asteroids[i].Speed;
                _asteroids[i].Rotation += _asteroids[i].RotationSpeed;

                _asteroids[i].Position.X = (_asteroids[i].Position.X + GraphicsDevice.Viewport.Width) % GraphicsDevice.Viewport.Width;
                _asteroids[i].Position.Y = (_asteroids[i].Position.Y + GraphicsDevice.Viewport.Height) % GraphicsDevice.Viewport.Height;

                _asteroids[i].BB = new BoundingBox(
                    new Vector3(_asteroids[i].Position.X - _asteroids[i].Size / 2, _asteroids[i].Position.Y - _asteroids[i].Size / 2, 0),
                    new Vector3(_asteroids[i].Position.X + _asteroids[i].Size / 2, _asteroids[i].Position.Y + _asteroids[i].Size / 2, 0));

                if (_asteroids[i].BB.Intersects(_ship.BB))
                {
                    _ship.Speed.X /= 2;
                    _ship.Speed.Y /= 2;
                    asteroidDestroyed = true;
                }

                if (!asteroidDestroyed)
                {
                    for (int j = 0; j < _rockets.Count; ++j)
                    {
                        _rockets[j].Position += _rockets[j].Speed;

                        _rockets[j].BB = new BoundingBox(
                            new Vector3(_rockets[j].Position.X - _rockets[j].Size / 2, _rockets[j].Position.Y - _rockets[j].Size / 2, 0),
                            new Vector3(_rockets[j].Position.X + _rockets[j].Size / 2, _rockets[j].Position.Y + _rockets[j].Size / 2, 0));

                        if (_rockets[j].BB.Intersects(_asteroids[i].BB))
                        {
                            asteroidDestroyed = true;
                            _rockets.Remove(_rockets[j]);
                        }
                    }
                }

                if (asteroidDestroyed)
                    _asteroids.Remove(_asteroids[i]);
            }

            for (int i = 0; i < _speedBonuses.Count; ++i)
            {
                _speedBonuses[i].BB = new BoundingBox(
                    new Vector3(_speedBonuses[i].Position.X - _speedBonuses[i].Size / 2, _speedBonuses[i].Position.Y - _speedBonuses[i].Size / 2, 0),
                    new Vector3(_speedBonuses[i].Position.X + _speedBonuses[i].Size / 2, _speedBonuses[i].Position.Y + _speedBonuses[i].Size / 2, 0));

                if (_speedBonuses[i].BB.Intersects(_ship.BB))
                {
                    _ship.Speed.X *= 2;
                    _ship.Speed.Y *= 2;
                    _speedBonuses.Remove(_speedBonuses[i]);
                }
            }


            base.Update(gameTime);
        }

        private void InputDeviceUpdate(GameTime gameTime)
        {
            _keyboardState = Keyboard.GetState();
            _gamePadState = GamePad.GetState(0);

            if (_keyboardState.IsKeyDown(Keys.Escape) || _gamePadState.IsButtonDown(Buttons.Back))
                this.Exit();

            var rotationSpeed = .05f;
            if (_keyboardState.IsKeyDown(Keys.Right) || _gamePadState.IsButtonDown(Buttons.DPadRight))
            {
                _ship.Rotate(rotationSpeed);
            }
            else if (_keyboardState.IsKeyDown(Keys.Left) || _gamePadState.IsButtonDown(Buttons.DPadLeft))
            {
                _ship.Rotate(-rotationSpeed);
            }

            var acceleration = 0.3f;
            if (_keyboardState.IsKeyDown(Keys.Up) || _gamePadState.IsButtonDown(Buttons.DPadUp))
            {
                _ship.SpeedUp(acceleration);
            }
            else if (_keyboardState.IsKeyDown(Keys.Down) || _gamePadState.IsButtonDown(Buttons.DPadDown))
            {
                _ship.SpeedDown(acceleration);
            }

            if ((_keyboardState.IsKeyDown(Keys.Space) && _oldKeyboardState.IsKeyUp(Keys.Space)) ||
                (_gamePadState.IsButtonDown(Buttons.A) && _oldGamePadState.IsButtonUp(Buttons.A)))
            {
                var kickRocket = 40f;
                var speedRocket = 0.9f;

                GameObject rocketAsteroid = new GameObject(10,
                    new Vector2(_ship.Position.X + (float)(kickRocket * Math.Sin(_ship.Rotation)), _ship.Position.Y - (float)(kickRocket * Math.Cos(_ship.Rotation))),
                    new Vector2(_ship.Speed.X + (float)(speedRocket * Math.Sin(_ship.Rotation)), _ship.Speed.Y - (float)(speedRocket * Math.Cos(_ship.Rotation))));

                _asteroids.Add(rocketAsteroid);
            }

            if ((_keyboardState.IsKeyDown(Keys.LeftShift) && _oldKeyboardState.IsKeyUp(Keys.LeftShift)) ||
                (_gamePadState.IsButtonDown(Buttons.B) && _oldGamePadState.IsButtonUp(Buttons.B)))
            {
                RocketWeapon rocket = new RocketWeapon(_ship.Position);
                _rockets.Add(rocket);
            }

            _oldKeyboardState = _keyboardState;
            _oldGamePadState = _gamePadState;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetTransform());

            for (int i = 0; i < _asteroids.Count; ++i)
            {
                _spriteBatch.Draw(_textureManager.MeteorTexture, _asteroids[i].Position, null, Color.White, _asteroids[i].Rotation, new Vector2(24, 24), new Vector2(_asteroids[i].Size / 48, _asteroids[i].Size / 48), SpriteEffects.None, 0f);
            }

            for (int i = 0; i < _speedBonuses.Count; ++i)
            {
                _spriteBatch.Draw(_textureManager.SpeedBonusTexture, _speedBonuses[i].Position, Color.White);
            }

            for (int i = 0; i < _rockets.Count; ++i)
            {
                _spriteBatch.Draw(_textureManager.RocketTexture, _rockets[i].Position, Color.White);
            }

            _spriteBatch.Draw(_textureManager.ShipTexture, _ship.Position, null, Color.White, _ship.Rotation, new Vector2(24, 24), new Vector2(_ship.Size / 48, _ship.Size / 48), SpriteEffects.None, 0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
