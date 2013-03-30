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
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static readonly int WorldWidth = 2048;
        public static readonly int WorldHeight = 2048;
        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Ship _ship;

        private Camera _camera;
        private InputDevices _inputDevices;

        World _world = new World();

        private Song _song;

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
        }

        protected override void Initialize()
        {
            Random r = new Random();

            InitializeInputDevices();

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

        private void InitializeInputDevices()
        {
            _inputDevices = new InputDevices(this);
            _inputDevices.Initialize();

            this.Activated += new EventHandler<EventArgs>(Game_Activated);
            this.Deactivated += new EventHandler<EventArgs>(Game_Deactivated);

            _inputDevices.AddAction(InputActionType.ExitGame, this.Exit);
            _inputDevices.AddAction(InputActionType.DebugMode,
                delegate
                {
                    GameOptions.IsDebugMode = !GameOptions.IsDebugMode;
                });
            _inputDevices.AddAction(InputActionType.PlayStopMediaPlayer,
                delegate
                {
                    if (MediaPlayer.Queue.ActiveSong != null)
                    {
                        if (MediaPlayer.State == MediaState.Paused)
                            MediaPlayer.Resume();
                        else
                            MediaPlayer.Pause();
                    }
                    else MediaPlayer.Play(_song);
                });

            var strafeAcceleration = 0.5f;
            _inputDevices.AddActionFloat(InputActionFloatType.MoveRightLeft,
                delegate(float sensitivity)
                {
                    _ship.Strafe(strafeAcceleration * sensitivity);
                });

            var moveAcceleration = 0.5f;
            _inputDevices.AddActionFloat(InputActionFloatType.MoveUpDown,
                delegate(float sensitivity)
                {
                    _ship.SpeedUp(moveAcceleration * sensitivity);
                });

            _inputDevices.AddAction(InputActionType.Strike,
                delegate
                {
                    var kickRocket = 40f;
                    var speedRocket = 14.9f;

                    PlasmaBullet plasmaBullet = new PlasmaBullet();
                    plasmaBullet.Size = 10;
                    plasmaBullet.Position = new Vector2(_ship.Position.X + kickRocket * Maf.Sin(_ship.Rotation), 
                        _ship.Position.Y - kickRocket * Maf.Cos(_ship.Rotation));
                    plasmaBullet.Speed = new Vector2(_ship.Speed.X + speedRocket * Maf.Sin(_ship.Rotation),
                        _ship.Speed.Y - speedRocket * Maf.Cos(_ship.Rotation));
                    _world.Add(plasmaBullet);
                });
            _inputDevices.AddAction(InputActionType.Rocket,
                delegate
                {
                    RocketWeapon rocket1 = new RocketWeapon(_ship.Position + new Vector2(-40f * Maf.Cos(_ship.Rotation), -40f * Maf.Sin(_ship.Rotation)), _ship.Speed, _ship.Rotation);
                    RocketWeapon rocket2 = new RocketWeapon(_ship.Position + new Vector2(40f * Maf.Cos(_ship.Rotation), 40f * Maf.Sin(_ship.Rotation)), _ship.Speed, _ship.Rotation);
                    _world.Add(rocket1);
                    _world.Add(rocket2);
                });
            _inputDevices.AddActionFloat(InputActionFloatType.Rotate,
                delegate(float dx)
                {
                    _ship.Rotate(dx * 0.1f);
                });
        }

        void Game_Activated(object sender, EventArgs e)
        {
            _inputDevices.StartUpdate();
        }

        void Game_Deactivated(object sender, EventArgs e)
        {
            _inputDevices.StopUpdate();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadTextures(Content);
            _song = Content.Load<Song>("Music/SOUP - Q7");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            _inputDevices.Update(gameTime);

            foreach (var obj in _world)
                obj.Update();

            Collisions.Detect(_world);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetParallaxTransform());
            _spriteBatch.Draw(TextureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(512, 512), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(TextureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(1536, 512), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(TextureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(1536, 1536), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(TextureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(512, 1536), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(TextureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(-512, 1536), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(TextureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(-512, 512), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(TextureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(-512, -512), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(TextureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(512, -512), SpriteEffects.None, 0.0f);
            _spriteBatch.Draw(TextureManager.SpaceTexture, new Rectangle(0, 0, 1024, 1024), null, Color.White, 0.0f, new Vector2(1536, -512), SpriteEffects.None, 0.0f);
            _spriteBatch.End();

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetTransform());

            foreach (var obj in _world)
                obj.Draw(_spriteBatch, gameTime);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
