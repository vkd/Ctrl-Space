using Ctrl_Space.Graphics;
using Microsoft.Xna.Framework;
using Ctrl_Space.Helpers;
using System;
using Ctrl_Space.Gameplay.Weapon;

namespace Ctrl_Space.Gameplay
{
    class EnemyShip : Ship
    {
        private Ship _target;
        private float _strafe = 0.0f;
        private float _accel = 0.0f;

        public EnemyShip()
        {
        }

        public void Reset(Vector2 position, World world, Ship target)
        {
            _target = target;
            base.Reset(position, world);

            _weapon = new PlasmaGun(this);
        }

        public Vector2 TargetPos;

        public override void Update(World world, Particles particles)
        {
            var d_pos = _target.Position - Position;
            float dx = ((d_pos.X + Game.WorldWidth / 2) % Game.WorldWidth) - Game.WorldWidth / 2;
            float dy = ((d_pos.Y + Game.WorldHeight / 2) % Game.WorldHeight) - Game.WorldHeight / 2;

            var directionTargetBegin = new Vector2(dx, dy);
            var nrm_directionTargetBegin = directionTargetBegin;
            nrm_directionTargetBegin.Normalize();

            //var time_Speed = directionTarget.Length() 
            //    / (
            //        Vector2.Dot(_target.Speed, new Vector2(-nrm_directionTarget.X, -nrm_directionTarget.Y))
            //        + Vector2.Dot(Speed, nrm_directionTarget)
            //    );

            //var directionTargetEnd = directionTargetBegin * Speed / (Speed - _target.Speed);
            //var nrm_directionTargetEnd = directionTargetEnd;
            //nrm_directionTargetEnd.Normalize();

            var time_weapon = directionTargetBegin.Length() / (_weapon.Speed + Vector2.Dot(Speed, nrm_directionTargetBegin));

            TargetPos = _target.Position + time_weapon * (_target.Speed - Speed);

            var t = directionTargetBegin - time_weapon * (Speed - _target.Speed);
            float ang = Maf.Atan2(t.Y, t.X);

            Rotation = ang + MathHelper.PiOver2;

            var dir = new Vector2(Maf.Cos(ang), Maf.Sin(ang));
            var nrm = new Vector2(dir.Y, -dir.X);
            var accel = Vector2.Dot(nrm_directionTargetBegin, dir);
            var strafe = Vector2.Dot(nrm_directionTargetBegin, nrm);

            //var distanceVector = new Vector2(directionTarget.X - time_Speed * (Speed.X - _target.Speed.X), directionTarget.Y - time_Speed * (Speed.Y - _target.Speed.Y));

            float distance_length = (directionTargetBegin).Length();
            //if (HP < (MaxHP / 2))
            //{
            //    ;
            //}
            //else 
            if (distance_length > 600.0f)
            {
                SpeedUp(accel);
                Strafe(strafe);
                //ShootAlt(Input.InputDigitalState.Released);
            }
            else if (distance_length > 400.0f)
            {
                _strafe = Chaos.GetFloat(-.2f, .2f);
                _accel = Chaos.GetFloat(-.05f, .05f);
                SpeedUp(0.4f * accel);
                Strafe(0.4f * strafe);
                //Shoot(Input.InputDigitalState.Released);
            }
            else if (distance_length < 200.0f)
            {
                _strafe = Chaos.GetFloat(-.2f, .2f);
                _accel = Chaos.GetFloat(-.05f, .05f);
                SpeedUp(-0.2f * accel);
                Strafe(-0.2f * strafe);
                //Shoot(Input.InputDigitalState.Pressed);
            }
            else if (distance_length < 100.0f)
            {
                SpeedUp(-accel);
                Strafe(-strafe);
                //Shoot(Input.InputDigitalState.Pressed);
            }
            else
            {
                SpeedUp(_accel);
                Strafe(_strafe);
                //ShootAlt(Input.InputDigitalState.Pressed);
            }

            Shoot(Input.InputDigitalState.Pressed);

            base.Update(world, particles);
        }

        public override MetaTexture GetTexture()
        {
            return TextureManager.EnemyTexture;
        }
    }
}
