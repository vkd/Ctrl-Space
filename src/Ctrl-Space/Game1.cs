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

        KeyboardState keyboardState;

        GameObject _ship;
        
        List<GameObject> _asteroids = new List<GameObject>();
        List<SpeedBonus> _speedBonuses = new List<SpeedBonus>();

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

            _ship = new GameObject();
            _ship.Size = 48;
            _ship.Position.X = maxWidth / 2;
            _ship.Position.Y = maxHeight / 2;

            for (int i = 0; i < 10; ++i)
            {
                GameObject asteroid = new GameObject();
                asteroid.Position.X = (float)(r.NextDouble() * maxWidth);
                asteroid.Position.Y = (float)(r.NextDouble() * maxHeight);
                asteroid.Speed.X = (float)(r.NextDouble() * 4 - 2);
                asteroid.Speed.Y = (float)(r.NextDouble() * 4 - 2);
                asteroid.Size = (float)(r.NextDouble() * 40 + 20);
                asteroid.Rotation = (float)(r.NextDouble() * 6.28);
                asteroid.RotationSpeed = (float)(r.NextDouble() * .1 - .05);
                _asteroids.Add(asteroid);

                if (i < 5)
                {
                    SpeedBonus bonus = new SpeedBonus();
                    bonus.Position.X = (float)(r.NextDouble() * maxWidth);
                    bonus.Position.Y = (float)(r.NextDouble() * maxHeight);
                    _speedBonuses.Add(bonus);
                }
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _myFirstTexture = Content.Load<Texture2D>("Bitmap1");
            _meteoriteTexture = Content.Load<Texture2D>("Bitmap2");
            _rocketTexture = Content.Load<Texture2D>("Rocket");
            _speedBonusTexture = Content.Load<Texture2D>("SpeedBonus");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                _ship.Rotation += .05f;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                _ship.Rotation -= .05f;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _ship.Speed.X += (float)(0.05f * Math.Sin(_ship.Rotation));
                _ship.Speed.Y -= (float)(0.05f * Math.Cos(_ship.Rotation));
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                _ship.Speed.X -= (float)(0.05f * Math.Sin(_ship.Rotation));
                _ship.Speed.Y += (float)(0.05f * Math.Cos(_ship.Rotation));
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                
            }

            _ship.Position += _ship.Speed;

            _ship.Speed *= 0.99f;

            _ship.Position.X = (_ship.Position.X + GraphicsDevice.Viewport.Width) % GraphicsDevice.Viewport.Width;
            _ship.Position.Y = (_ship.Position.Y + GraphicsDevice.Viewport.Height) % GraphicsDevice.Viewport.Height;
            _ship.BB.Min = new Vector3(_ship.Position.X - _ship.Size / 2, _ship.Position.Y - _ship.Size / 2, 0);
            _ship.BB.Max = new Vector3(_ship.Position.X - _ship.Size / 2, _ship.Position.Y + _ship.Size / 2, 0);

            for (int i = 0; i < _asteroids.Count; ++i)
            {
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
                    _asteroids.Remove(_asteroids[i]);
                }
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            for (int i = 0; i < _asteroids.Count; ++i)
            {
                _spriteBatch.Draw(_meteoriteTexture, _asteroids[i].Position, null, Color.White, _asteroids[i].Rotation, new Vector2(24, 24), new Vector2(_asteroids[i].Size / 48, _asteroids[i].Size / 48), SpriteEffects.None, 0f);
            }

            for (int i = 0; i < _speedBonuses.Count; ++i)
            {
                _spriteBatch.Draw(_speedBonusTexture, _speedBonuses[i].Position, Color.White);
            }

            _spriteBatch.Draw(_myFirstTexture, _ship.Position, null, Color.White, _ship.Rotation, new Vector2(24, 24), new Vector2(_ship.Size / 48, _ship.Size / 48), SpriteEffects.None, 0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
