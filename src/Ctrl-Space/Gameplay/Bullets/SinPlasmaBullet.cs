using System;
using Microsoft.Xna.Framework;
using Ctrl_Space.Helpers;

namespace Ctrl_Space.Gameplay.Bullets
{
    class SinPlasmaBullet : PlasmaBullet
    {
        public Vector2 StartSpeed;
        public float SinState = 0;

        public override void Reset()
        {
            SinState = MathHelper.PiOver2;//Chaos.GetFloat(MathHelper.TwoPi);
            base.Reset();
        }

        public override void Update(World world, Graphics.Particles particles)
        {
            float step = 0.6f;
            SinState += step;

            Vector2 ve = new Vector2(StartSpeed.Y, -StartSpeed.X);
            ve.Normalize();

            Position += ve * Maf.Sin(SinState) * 40;

            base.Update(world, particles);
        }
    }
}
