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
        public static readonly int WorldWidth = 2048;
        public static readonly int WorldHeight = 2048;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //Keyboard
        private KeyboardState _keyboardState;
        private KeyboardState _oldKeyboardState;

        //GamePad
        private GamePadState _gamePadState;
        private GamePadState _oldGamePadState;

        //Mouse
        private MouseState _mouseState;
        private MouseState _oldMouseState;

        private Ship _ship;

        private Camera _camera;

        World _world = new World();

        private Song _song;

        private TextureManager _textureManager;

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

            _oldMouseState = new MouseState();

            _ship = new Ship(new Vector2(WorldWidth / 2, WorldHeight / 2));
            _camera = new Camera(_ship);

            for (int i = 0; i < 10; ++i)
            {
                Asteroid asteroid = new Asteroid();
                asteroid.Size = (float)(r.NextDouble() * 60 + 20);
                asteroid.Mass = asteroid.Size;
                asteroid.Position = new Vector2((float)(r.NextDouble() * WorldWidth), (float)(r.NextDouble() * WorldHeight));
                asteroid.Speed = new Vector2((float)(r.NextDouble() * 4 - 2), (float)(r.NextDouble() * 4 - 2));
                asteroid.Rotation = (float)(r.NextDouble() * 6.28);
                asteroid.RotationSpeed = (float)(r.NextDouble() * .1 - .05);
                _world.Add(asteroid);
            }

            for (int i = 0; i < 5; ++i)
            {
                SpeedBonus bonus = new SpeedBonus(
                    new Vector2((float)(r.NextDouble() * WorldWidth),
                    (float)(r.NextDouble() * WorldHeight)));
                _world.Add(bonus);
            }

            _world.Add(_ship);

            Components.Add(new FPS(this, "Fonts/FPSFont", Vector2.Zero));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureManager = new TextureManager(Content);
            _song = Content.Load<Song>("Music/SOUP - Q7");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            InputDeviceUpdate(gameTime);

            foreach (var obj in _world)
                obj.Update();

            Collisions.Detect(_world);

            base.Update(gameTime);
        }

        private void InputDeviceUpdate(GameTime gameTime)
        {
            _keyboardState = Keyboard.GetState();
            _gamePadState = GamePad.GetState(0);
            _mouseState = Mouse.GetState();

            if (_keyboardState.IsKeyDown(Keys.Escape) || _gamePadState.IsButtonDown(Buttons.Back))
                this.Exit();

            if (_keyboardState.IsKeyUp(Keys.F1) && _oldKeyboardState.IsKeyDown(Keys.F1))
                GameOptions.IsDebugMode = !GameOptions.IsDebugMode;

            if (_keyboardState.IsKeyUp(Keys.Tab) && _oldKeyboardState.IsKeyDown(Keys.Tab))
                if (MediaPlayer.Queue.ActiveSong != null)
                {
                    if (MediaPlayer.State == MediaState.Paused)
                        MediaPlayer.Resume();
                    else
                        MediaPlayer.Pause();
                }
                else MediaPlayer.Play(_song);

            var acceleration = 0.5f;
            if (_keyboardState.IsKeyDown(Keys.Right) || _gamePadState.IsButtonDown(Buttons.DPadRight) || _keyboardState.IsKeyDown(Keys.D))
            {
                _ship.Strafe(acceleration);
            }
            else if (_keyboardState.IsKeyDown(Keys.Left) || _gamePadState.IsButtonDown(Buttons.DPadLeft) || _keyboardState.IsKeyDown(Keys.A))
            {
                _ship.Strafe(-acceleration);
            }

            if (_keyboardState.IsKeyDown(Keys.Up) || _gamePadState.IsButtonDown(Buttons.DPadUp) || _keyboardState.IsKeyDown(Keys.W))
            {
                _ship.SpeedUp(acceleration);
            }
            else if (_keyboardState.IsKeyDown(Keys.Down) || _gamePadState.IsButtonDown(Buttons.DPadDown) || _keyboardState.IsKeyDown(Keys.S))
            {
                _ship.SpeedUp(-acceleration);
            }

            if ((_keyboardState.IsKeyDown(Keys.Space) && _oldKeyboardState.IsKeyUp(Keys.Space)) ||
                (_gamePadState.IsButtonDown(Buttons.A) && _oldGamePadState.IsButtonUp(Buttons.A)) ||
                (_mouseState.LeftButton == ButtonState.Pressed && _oldMouseState.LeftButton == ButtonState.Released))
            {
                var kickRocket = 40f;
                var speedRocket = 4.9f;

                PlasmaBullet plasmaBullet = new PlasmaBullet();
                plasmaBullet.Size = 10;
                plasmaBullet.Position = new Vector2(_ship.Position.X + (float)(kickRocket * Math.Sin(_ship.Rotation)), _ship.Position.Y - (float)(kickRocket * Math.Cos(_ship.Rotation)));
                plasmaBullet.Speed = new Vector2(_ship.Speed.X + (float)(speedRocket * Math.Sin(_ship.Rotation)), _ship.Speed.Y - (float)(speedRocket * Math.Cos(_ship.Rotation)));
                _world.Add(plasmaBullet);
            }

            if ((_keyboardState.IsKeyDown(Keys.LeftShift) && _oldKeyboardState.IsKeyUp(Keys.LeftShift)) ||
                (_gamePadState.IsButtonDown(Buttons.B) && _oldGamePadState.IsButtonUp(Buttons.B)))
            {
                RocketWeapon rocket = new RocketWeapon(_ship.Position, _ship.Rotation);
                _world.Add(rocket);
            }

            _ship.Rotate((_oldMouseState.X - _mouseState.X) * -0.002f);

            _oldKeyboardState = _keyboardState;
            _oldGamePadState = _gamePadState;
            _oldMouseState = _mouseState;
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetParallaxTransform());
            _spriteBatch.Draw(_textureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(512, 512), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(_textureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(1536, 512), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(_textureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(1536, 1536), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(_textureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(512, 1536), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(_textureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(-512, 1536), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(_textureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(-512, 512), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(_textureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(-512, -512), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(_textureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(512, -512), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(_textureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(1536, -512), SpriteEffects.None, 0.0f);
            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetTransform());

            foreach (var obj in _world)
                obj.Draw(_spriteBatch, _textureManager);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
