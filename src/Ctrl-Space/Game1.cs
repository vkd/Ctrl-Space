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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D _myFirstTexture;
        Texture2D _meteoriteTexture;

        Rectangle myPosition;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
        }

        protected override void Initialize()
        {
            myPosition = new Rectangle(0, 0, 48, 48);

            Random r = new Random();

            int maxWidth = GraphicsDevice.Viewport.Width;
            int maxHeight = GraphicsDevice.Viewport.Height;

            for (int i = 0; i < 100; ++i)
            {
                int size = r.Next(10,100);
                _listMeteorites.Add(new Rectangle(r.Next(maxWidth), r.Next(maxHeight), size, size));
                _listMeteoritesV.Add(new Vector2((float)(r.NextDouble() * 6 - 3), (float)(r.NextDouble() * 6 - 3)));
                _listMeteoritesR.Add((float)(r.NextDouble() * 6.28));
                _listMeteoritesRV.Add((float)(r.NextDouble() * .2 - .1));
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _myFirstTexture = Content.Load<Texture2D>("Bitmap1");
            _meteoriteTexture = Content.Load<Texture2D>("Bitmap2");
        }

        List<Rectangle> _listMeteorites = new List<Rectangle>();
        List<Vector2> _listMeteoritesV = new List<Vector2>();
        List<float> _listMeteoritesR = new List<float>();
        List<float> _listMeteoritesRV = new List<float>();

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
                myPosition.X += 5;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                myPosition.X -= 5;
            }

            myPosition.X = (myPosition.X + GraphicsDevice.Viewport.Width) % GraphicsDevice.Viewport.Width;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                myPosition.Y -= 5;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                myPosition.Y += 5;
            }

            myPosition.Y = (myPosition.Y + GraphicsDevice.Viewport.Height) % GraphicsDevice.Viewport.Height;

            oldKeyboardState = keyboardState;

            for (int i = 0; i < _listMeteorites.Count; ++i)
            {
                _listMeteorites[i] = new Rectangle((_listMeteorites[i].X + (int)_listMeteoritesV[i].X + GraphicsDevice.Viewport.Width) % GraphicsDevice.Viewport.Width
                    , (_listMeteorites[i].Y + (int)_listMeteoritesV[i].Y + GraphicsDevice.Viewport.Height) % GraphicsDevice.Viewport.Height
                    , _listMeteorites[i].Width
                    , _listMeteorites[i].Height);
                _listMeteoritesR[i] += _listMeteoritesRV[i];
            }

            base.Update(gameTime);
        }

        float x = 0.0f;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            for (int i = 0; i < _listMeteorites.Count; ++i)
            {
                spriteBatch.Draw(_meteoriteTexture, _listMeteorites[i], null, Color.White, _listMeteoritesR[i], new Vector2(24, 24), SpriteEffects.None, 0.0f);
            }

            spriteBatch.Draw(_myFirstTexture, myPosition, null, Color.White, x, new Vector2(24,24), SpriteEffects.None, 0.0f);
            x += .1f;

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
