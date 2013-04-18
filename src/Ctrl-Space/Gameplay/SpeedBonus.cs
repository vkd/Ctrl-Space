using Ctrl_Space.Graphics;
using Ctrl_Space.Physics;
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

        public override void Update(World world, Particles particles)
        {
            foreach (var col in Collisions)
                Collided(col, world, particles);
            base.Update(world, particles);
        }

        private void Collided(Collision col, World world, Particles particles)
        {
            if (col.GameObject is Ship)
                IsDestroyed = true;
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.SpeedBonusTexture;
        }
    }
}
