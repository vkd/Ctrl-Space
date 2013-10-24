using Ctrl_Space.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Gameplay.Bullets
{
    class PlasmaBullet : GameObject
    {
        private float State = 1.0f;

        public void Reset()
        {
            State = 1.0f;
        }

        public override MetaTexture GetTexture()
        {
            return TextureManager.PlasmaBulletTexture;
        }

        public override void Update(World world, Particles particles)
        {
            foreach (var col in Collisions)
                Collided(col.GameObject, world, particles);
            base.Update(world, particles);
            State -= .01f;
            if (State < 0f)
                IsDestroyed = true;
            particles.Emit(ParticleManager.PlasmaTrail, Position, Vector2.Zero);
        }

        public void Collided(GameObject go, World world, Particles particles)
        {
            if (!(go is PlasmaBullet))
                IsDestroyed = true;
        }
    }
}
