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

        KeyboardState keyboardState;

        GameObject _ship;
        List<GameObject> _asteroids = new List<GameObject>();

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

            for (int i = 0; i < 100; ++i)
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
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _myFirstTexture = Content.Load<Texture2D>("Bitmap1");
            _meteoriteTexture = Content.Load<Texture2D>("Bitmap2");
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
                _ship.Position.X += 5;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                _ship.Position.X -= 5;
            }

            _ship.Position.X = (_ship.Position.X + GraphicsDevice.Viewport.Width) % GraphicsDevice.Viewport.Width;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _ship.Position.Y -= 5;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                _ship.Position.Y += 5;
            }

            _ship.Position.Y = (_ship.Position.Y + GraphicsDevice.Viewport.Height) % GraphicsDevice.Viewport.Height;

            _ship.Rotation += .08f;

            for (int i = 0; i < _asteroids.Count; ++i)
            {
                _asteroids[i].Position += _asteroids[i].Speed;
                _asteroids[i].Rotation += _asteroids[i].RotationSpeed;

                _asteroids[i].Position.X = (_asteroids[i].Position.X + GraphicsDevice.Viewport.Width) % GraphicsDevice.Viewport.Width;
                _asteroids[i].Position.Y = (_asteroids[i].Position.Y + GraphicsDevice.Viewport.Height) % GraphicsDevice.Viewport.Height;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            for (int i = 0; i < _asteroids.Count; ++i)
            {
                _spriteBatch.Draw(_meteoriteTexture, new Rectangle((int)_asteroids[i].Position.X, (int)_asteroids[i].Position.Y, (int)_asteroids[i].Size, (int)_asteroids[i].Size), null, Color.White, _asteroids[i].Rotation, new Vector2(24, 24), SpriteEffects.None, 0.0f);
            }

            _spriteBatch.Draw(_myFirstTexture, new Rectangle((int)_ship.Position.X, (int)_ship.Position.Y, (int)_ship.Size, (int)_ship.Size), null, Color.White, _ship.Rotation, new Vector2(24, 24), SpriteEffects.None, 0.0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
