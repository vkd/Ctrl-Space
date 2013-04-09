using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
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
