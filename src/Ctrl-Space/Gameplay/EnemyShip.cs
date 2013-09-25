using Ctrl_Space.Graphics;
using Microsoft.Xna.Framework;
using Ctrl_Space.Helpers;
using System;

namespace Ctrl_Space.Gameplay
{
    class EnemyShip : Ship
    {
        private Ship _target;
        private float _strafe = 0.0f;
        private float _accel = 0.0f;

        public EnemyShip(Vector2 position, World world, Ship target)
            : base(position, world)
        {
            _target = target;
        }

        public override void Update(World world, Particles particles)
        {
            float dx = _target.Position.X - Position.X;
            float dy = _target.Position.Y - Position.Y;
            if (dx > Game.WorldWidth / 2)
                dx -= Game.WorldWidth;
            if (dy > Game.WorldHeight / 2)
                dy -= Game.WorldHeight;
            if (dx < -Game.WorldWidth / 2)
                dx += Game.WorldWidth;
            if (dy < -Game.WorldHeight / 2)
                dy += Game.WorldHeight;

            var vect = new Vector2(dx, dy);

            var move = new Vector2(vect.X, vect.Y);
            move.Normalize();
            float ang = Maf.Atan2(vect.Y, vect.X);
            Rotation = ang + MathHelper.PiOver2;

            var dir = new Vector2(Maf.Cos(ang), Maf.Sin(ang));
            var nrm = new Vector2(dir.Y, -dir.X);
            var accel = Vector2.Dot(move, dir);
            var strafe = Vector2.Dot(move, nrm);

            float distance = (vect).Length();
            if (distance > 600.0f)
            {
                SpeedUp(accel);
                Strafe(strafe);
                ShootAlt(Input.InputDigitalState.Released);
            }
            else if (distance > 400.0f)
            {
                _strafe = Chaos.GetFloat(-.2f, .2f);
                _accel = Chaos.GetFloat(-.05f, .05f);
                SpeedUp(0.4f * accel);
                Strafe(0.4f * strafe);
                Shoot(Input.InputDigitalState.Released);
            }
            else if (distance < 200.0f)
            {
                _strafe = Chaos.GetFloat(-.2f, .2f);
                _accel = Chaos.GetFloat(-.05f, .05f);
                SpeedUp(-0.2f * accel);
                Strafe(-0.2f * strafe);
                Shoot(Input.InputDigitalState.Pressed);
            }
            else if (distance < 100.0f)
            {
                SpeedUp(-accel);
                Strafe(-strafe);
                Shoot(Input.InputDigitalState.Pressed);
            }
            else
            {
                SpeedUp(_accel);
                Strafe(_strafe);
                ShootAlt(Input.InputDigitalState.Pressed);
            }
            
            base.Update(world, particles);
        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D GetTexture()
        {
            return TextureManager.EnemyTexture;
        }
    }
}
