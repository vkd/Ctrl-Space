using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Gameplay
{
    class SpeedBonus : GameObject
    {
        public SpeedBonus(Vector2 position)
        {
            Position = position;
            Speed = Vector2.Zero;
            Size = 15;
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.SpeedBonusTexture;
        }
    }
}
