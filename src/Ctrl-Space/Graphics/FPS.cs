using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Graphics
{
    class FPS : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        private string _spriteFontName;
        private Vector2 _position;
        private int _fps = 0;
        private int _currentFps = 0;
        private double _timeFromLastFrame = 0;
        private bool _isVisible = false;

        public FPS(Game game, string fontName, Vector2 position)
            : base(game)
        {
            _spriteFontName = fontName;
            _position = position;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Game.Content.Load<SpriteFont>(_spriteFontName); 
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            _isVisible = GameOptions.IsDebugMode;
            if (_isVisible)
            {
                _timeFromLastFrame += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (_timeFromLastFrame >= 1000)
                {
                    _fps = _currentFps;
                    _currentFps = 0;
                    _timeFromLastFrame = 0;
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_isVisible)
            {
                _currentFps++;
                _spriteBatch.Begin();
                _spriteBatch.DrawString(_spriteFont, "FPS: " + _fps, _position + Vector2.One, Color.Black);
                if (_fps > 10)
                    _spriteBatch.DrawString(_spriteFont, "FPS: " + _fps, _position, Color.White);
                else
                    _spriteBatch.DrawString(_spriteFont, "FPS: " + _fps, _position, Color.Red); _spriteBatch.End();
            }
        }
    }
}
