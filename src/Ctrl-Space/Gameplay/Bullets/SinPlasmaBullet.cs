using System;
using Microsoft.Xna.Framework;
using Ctrl_Space.Helpers;

namespace Ctrl_Space.Gameplay.Bullets
{
    class SinPlasmaBullet : PlasmaBullet
    {
        private float sinState = 0;//Chaos.GetFloat(MathHelper.TwoPi);

        public Vector2 StartSpeed;

        public override void Update(World world, Graphics.Particles particles)
        {
            float step = 0.2f;
            sinState += step;

            Vector2 ve = new Vector2(StartSpeed.Y, -StartSpeed.X);
            ve.Normalize();

            Speed = Speed + ve * Maf.Cos(sinState);

            base.Update(world, particles);
        }
    }
}
