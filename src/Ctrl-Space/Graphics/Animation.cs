using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Graphics
{
    class Animation
    {
        private int _frameCount; // количество всех фреймов в изображении
        private int _frame; // какой фрейм нарисован в данный момент 
        private float _fimeForFrame; // cколько времени нужно показывать один фрейм
        private float _totalTime; // сколько времени прошло с показа предыдущего фрейма
        private Texture2D _texture;

        public Animation()
        {
            _frame = 0;
            _fimeForFrame = 0.25f;
            _totalTime = 0;
        }

        public Rectangle GetAnimation(GameTime gameTime, Texture2D texture)
        {
            _texture = texture;
            _frameCount = _texture.Width / _texture.Height;
            _totalTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_frameCount != 1)
            {
                if (_frame == _frameCount - 1)
                    _frame = 0;
                if (_totalTime > _fimeForFrame)
                {
                    _frame++;
                    //_frame = _frame % (_frameCount - 1);
                    _totalTime = 0;
                }
            }

            int frameWidth = _texture.Width / _frameCount;
            return new Rectangle(frameWidth * _frame, 0, frameWidth, _texture.Height);
        }
    }
}
