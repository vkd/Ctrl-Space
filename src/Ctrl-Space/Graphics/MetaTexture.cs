using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Graphics
{
    public class MetaTexture
    {
        private Texture2D _texture;
        private Rectangle _region;

        public MetaTexture(Texture2D texture)
        {
            _texture = texture;
            _region = texture.Bounds;
        }

        public MetaTexture(Texture2D texture, Rectangle region)
        {
            _texture = texture;
            _region = region;
        }

        public Texture2D Texture { get { return _texture; } }
        public Rectangle Region { get { return _region; } }
    }
}
