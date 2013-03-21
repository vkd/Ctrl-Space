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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
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

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _myFirstTexture = Content.Load<Texture2D>("Bitmap1");
            _meteoriteTexture = Content.Load<Texture2D>("Bitmap2");
            // TODO: use this.Content to load your game content here
        }

        List<Rectangle> _listMeteorites = new List<Rectangle>();
        List<Vector2> _listMeteoritesV = new List<Vector2>();
        List<float> _listMeteoritesR = new List<float>();
        List<float> _listMeteoritesRV = new List<float>();

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            // Allows the game to exit
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

            // TODO: Add your update logic here

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

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // TODO: Add your drawing code here
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
