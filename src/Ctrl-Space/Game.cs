using Ctrl_Space.Gameplay;
using Ctrl_Space.Graphics;
using Ctrl_Space.Helpers;
using Ctrl_Space.Input;
using Ctrl_Space.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonogameTest.Graphics;
using System;

namespace Ctrl_Space
{
    class Game : Microsoft.Xna.Framework.Game
    {
        public static readonly GameObjects Objects = new GameObjects();

        public static readonly int WorldWidthInClusters = 64;
        public static readonly int WorldHeihgtInClusters = 64;
        public static readonly int ClusterSizeInPowerOfTwo = 8;
        public static readonly int ClusterSize = 1 << ClusterSizeInPowerOfTwo;
        public static readonly int WorldWidth = WorldWidthInClusters * ClusterSize;
        public static readonly int WorldHeight = WorldHeihgtInClusters * ClusterSize;

        public static int ResolutionX = 1024;
        public static int ResolutionY = 768;
        public static float ViewDistance = 600f;

        public GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Ship _ship;
        private EnemyShip _enemyShip;

        private Camera _camera;
        private InputManager _inputManager = null;

        private World _world = new World();

        private Particles _particles = new Particles();

        private WorldLoop _worldLoop = new WorldLoop();
        private WorldLoop _worldLoopParticles = new WorldLoop();

        private Song _song;

        public static Config Config = new Config("config.cfg");

        private DebugGeometry _debugGeometry;
        public static readonly DebugConsole DebugConsole = new DebugConsole();

        public Background _background = new Background { RepeatX = 8, RepeatY = 8 };

        public SimpleFont _font = new SimpleFont();

        public bool IsStartTimer = true;
        public int CountStartTimer = 3;
        public TimeSpan LastTimeSpan = new TimeSpan(0);
        private float _timerFontSize = 0f;

        public static int PlayerWins = 0;
        public static int EnemyShipWins = 0;

        public Game()
        {
            DebugConsole.Append("Init game...").NewLine();
            _graphics = new GraphicsDeviceManager(this);
            DebugConsole.Append("Init gfx...").NewLine();
            Content.RootDirectory = "Content";
            DebugConsole.Append("Loading config...").NewLine();

            ResolutionX = Config.Resolution.X;
            ResolutionY = Config.Resolution.Y;

            _graphics.PreferredBackBufferWidth = ResolutionX;
            _graphics.PreferredBackBufferHeight = ResolutionY;
            _graphics.ApplyChanges();
            ViewDistance = Maf.Sqrt((ResolutionX * ResolutionX + ResolutionY * ResolutionY) * 0.3f);
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = Config.IsFullScreen;
            _graphics.ApplyChanges();

            InitializeInputManager();

            _ship = Game.Objects.CreateShip(new Vector2(WorldWidth / 2, WorldHeight / 2), _world);
            _enemyShip = Game.Objects.CreateEnemyShip(new Vector2(Chaos.GetFloat(0, WorldWidth), Chaos.GetFloat(0, WorldHeight)), _world, _ship);
            _ship.HP = 100;
            _ship.MaxHP = 100;
            _enemyShip.HP = 100;
            _enemyShip.MaxHP = 100;
            _camera = new Camera(_ship);

            for (int i = 0; i < 100; ++i)
            {
                Asteroid asteroid = Game.Objects.CreateAsteroid();
                asteroid.Size = Chaos.GetFloat(20f, 80f);
                asteroid.Mass = asteroid.Size;
                asteroid.HP = (int)asteroid.Size / 8;
                asteroid.MaxHP = (int)asteroid.Size / 8;
                asteroid.Position = Chaos.GetVector2InRectangle(WorldWidth, WorldHeight);
                asteroid.Speed = Chaos.GetVector2InCenterRectangle(2f, 2f);
                asteroid.Rotation = Chaos.GetFloat(MathHelper.TwoPi);
                asteroid.RotationSpeed = Chaos.GetFloat(-.05f, .05f);
                _world.Add(asteroid);
            }

            for (int i = 0; i < 150; ++i)
            {
                SpeedBonus bonus = Game.Objects.CreateSpeedBonus(Chaos.GetVector2InRectangle(WorldWidth, WorldHeight));
                _world.Add(bonus);
            }

            for (int i = 0; i < 150; ++i)
            {
                Medkit medkit = Game.Objects.CreateMedkit(Chaos.GetVector2InRectangle(WorldWidth, WorldHeight));
                _world.Add(medkit);
            }

            _world.Add(_enemyShip);
            _world.Add(_ship);

            Components.Add(new FPS(this, Vector2.Zero));

            _worldLoopParticles.Clusterize(_particles.ParticlesList);
            _worldLoop.Clusterize(_world.GameObjects);

            base.Initialize();
        }

        private void InitializeInputManager()
        {
            _inputManager = new InputManager(this);
            _inputManager.StartUpdate();

            this.Activated += new EventHandler<EventArgs>(Game_Activated);
            this.Deactivated += new EventHandler<EventArgs>(Game_Deactivated);

            _inputManager.ExitGame += () => Exit();
            _inputManager.DebugMode += () => GameOptions.IsDebugMode = !GameOptions.IsDebugMode;
            _inputManager.PlayStopMediaPlayer += () =>
                {
                    if (MediaPlayer.Queue.ActiveSong != null)
                    {
                        if (MediaPlayer.State == MediaState.Paused)
                            MediaPlayer.Resume();
                        else
                            MediaPlayer.Pause();
                    }
                    else MediaPlayer.Play(_song);
                };

            var strafeAcceleration = 0.5f;
            _inputManager.MoveRightLeft += e => _ship.Strafe(strafeAcceleration * e.Value);

            var moveAcceleration = 0.5f;
            _inputManager.MoveUpDown += e => _ship.SpeedUp(moveAcceleration * e.Value);

            _inputManager.PrimaryWeapon += e => _ship.Shoot(e.State);
            _inputManager.SecondaryWeapon += e => _ship.ShootAlt(e.State);

            _inputManager.Rotate += e => _ship.Rotate(e.Value * 0.1f);
        }

        void Game_Activated(object sender, EventArgs e)
        {
            this.IsMouseVisible = false;
            _inputManager.StartUpdate();
        }

        void Game_Deactivated(object sender, EventArgs e)
        {
            this.IsMouseVisible = true;
            _inputManager.StopUpdate();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _debugGeometry = new DebugGeometry(GraphicsDevice);
            TextureManager.LoadTextures(GraphicsDevice, Content);
            //_song = Content.Load<Song>("Music/SOUP - Q7");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (IsStartTimer)
            {
                var diff = gameTime.TotalGameTime.TotalMilliseconds - LastTimeSpan.TotalMilliseconds;
                _timerFontSize = (float)(diff + 20.0);
                if (diff > 1000)
                {
                    _timerFontSize = 20f;
                    LastTimeSpan = gameTime.TotalGameTime;
                    CountStartTimer -= 1;
                    if (CountStartTimer < 0)
                    {
                        IsStartTimer = false;
                    }
                }
            }

            _inputManager.Update(gameTime);

            if (!IsStartTimer)
            {
                Collisions.Detect(_worldLoop.Clusters, _world);

                _world.Update(_world, _particles);

                _particles.Update(_world, _particles);
            }


            _worldLoopParticles.Clusterize(_particles.ParticlesList);
            _worldLoop.Clusterize(_world.GameObjects);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _debugGeometry.Prepare(_camera.GetTransform());

            _background.Draw(_spriteBatch, _camera);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _camera.GetTransform());

            _worldLoopParticles.PrepareClustersForRender(_camera.FollowedObject.Position, ViewDistance);
            while (_worldLoopParticles.FetchClustersForRender())
            {
                Vector2 offset = new Vector2(_worldLoopParticles.Cluster.ShiftX * WorldWidth, _worldLoopParticles.Cluster.ShiftY * WorldHeight);
                foreach (var obj in _worldLoopParticles.Cluster.GameObjects)
                    obj.Draw(_spriteBatch, gameTime, offset, _camera, _debugGeometry);
            }

            _worldLoop.PrepareClustersForRender(_camera.FollowedObject.Position, ViewDistance);
            while (_worldLoop.FetchClustersForRender())
            {
                Vector2 offset = new Vector2(_worldLoop.Cluster.ShiftX * WorldWidth, _worldLoop.Cluster.ShiftY * WorldHeight);
                foreach (var obj in _worldLoop.Cluster.GameObjects)
                    obj.Draw(_spriteBatch, gameTime, offset, _camera, _debugGeometry);
            }

            _spriteBatch.End();

            _spriteBatch.Begin(0, null, null, null, null, TextureManager.SDFFontEffect);
            if (IsStartTimer)
            {
                _font.DrawText(_spriteBatch, CountStartTimer == 0 ? "GO" : CountStartTimer.ToString(), _timerFontSize, new Vector2(GraphicsDevice.Viewport.Width / 2 - (CountStartTimer != 0 ? (_timerFontSize / 2) : _timerFontSize), GraphicsDevice.Viewport.Height / 2 - _timerFontSize / 2), Color.White);
            }
            var fontSizeHP = Game.ResolutionX / 68f;
            var lineHPText = 0;
            _font.DrawText(_spriteBatch, "HP:", fontSizeHP, new Vector2(16, 16 + fontSizeHP * lineHPText++), Color.White);
            _font.DrawText(_spriteBatch, "Ship - " + _ship.HP, fontSizeHP, new Vector2(16, 16 + fontSizeHP * lineHPText++), Color.Green);
            _font.DrawText(_spriteBatch, "EnemyShip - " + _enemyShip.HP, fontSizeHP, new Vector2(16, 16 + fontSizeHP * lineHPText++), Color.Red);
            lineHPText++;

            _font.DrawText(_spriteBatch, "Score:", fontSizeHP, new Vector2(16, 16 + fontSizeHP * lineHPText++), Color.White);
            _font.DrawText(_spriteBatch, "Ship - " + PlayerWins, fontSizeHP, new Vector2(16, 16 + fontSizeHP * lineHPText++), Color.Green);
            _font.DrawText(_spriteBatch, "EnemyShip - " + EnemyShipWins, fontSizeHP, new Vector2(16, 16 + fontSizeHP * lineHPText++), Color.Red);
            _spriteBatch.End();

            // ======= DEBUG INFO =======
            int k = (WorldWidth > WorldHeight ? WorldWidth : WorldHeight) / 256;
            int widthMap = (int)WorldWidth / k;
            int heightMap = (int)WorldHeight / k;

            Rectangle rectMap = new Rectangle(GraphicsDevice.Viewport.Width - 10 - widthMap, GraphicsDevice.Viewport.Height - 10 - heightMap, widthMap, heightMap);
            Vector2 vectorShip = new Vector2(rectMap.X + _ship.Position.X / k, rectMap.Y + _ship.Position.Y / k);
            Vector2 vectorEnemyShip = new Vector2(rectMap.X + _enemyShip.Position.X / k, rectMap.Y + _enemyShip.Position.Y / k);

            var dir = new Vector2(Maf.Cos(_ship.Rotation), Maf.Sin(_ship.Rotation));
            var nrm = new Vector2(dir.Y, -dir.X);

            _debugGeometry.Prepare(Matrix.Identity);
            _debugGeometry.DrawRectangle(rectMap, Color.Blue);
            _debugGeometry.DrawCircle(vectorShip, 2, Color.Green);
            _debugGeometry.DrawLine(vectorShip, nrm * 10 + vectorShip, Color.Green);
            _debugGeometry.DrawCircle(vectorEnemyShip, 2, Color.Red);

            _debugGeometry.Prepare(_camera.GetTransform());
            _debugGeometry.DrawLine(new Vector2(-WorldWidth, 0f), new Vector2(2f * WorldWidth, 0f), Color.Gray);
            _debugGeometry.DrawLine(new Vector2(-WorldWidth, WorldHeight), new Vector2(2f * WorldWidth, WorldHeight), Color.Gray);
            _debugGeometry.DrawLine(new Vector2(0f, -WorldHeight), new Vector2(0f, 2f * WorldHeight), Color.Gray);
            _debugGeometry.DrawLine(new Vector2(WorldWidth, -WorldHeight), new Vector2(WorldWidth, 2f * WorldHeight), Color.Gray);

            _debugGeometry.DrawCircle(_enemyShip.TargetPos, 24f, Color.Red);
            _debugGeometry.DrawCircle(_enemyShip.Position, 24f, Color.Green);
            _debugGeometry.DrawLine(_enemyShip.Position, _enemyShip.TargetPos, Color.Green);
            _debugGeometry.DrawCircle(_ship.Position, 24f, Color.Blue);
            _debugGeometry.DrawLine(_ship.Position, 32f, _ship.Rotation - MathHelper.PiOver2, Color.Blue);

            _spriteBatch.Begin(0, null, null, null, null, TextureManager.SDFFontEffect);
            DebugConsole.Draw(_spriteBatch, _font);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void WinShip(Type typeOfShip)
        {
            Game.DebugConsole.Append(typeOfShip == typeof(EnemyShip) ? "Player win!" : "CPU win!").NewLine();
            if (typeOfShip == typeof(EnemyShip))
                PlayerWins++;
            else
                EnemyShipWins++;
        }
    }
}
