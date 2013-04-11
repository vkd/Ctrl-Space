using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.GameClasses.Bullets
{
    class PlasmaBullet : GameObject
    {
        private float State = 1.0f;

        public override Texture2D GetTexture()
        {
            return TextureManager.PlasmaBulletTexture;
        }

        public override void Update()
        {
            base.Update();
            State -= .01f;
            if (State < 0f)
                IsDestroyed = true;
        }
    }
}
