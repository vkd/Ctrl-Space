﻿using Ctrl_Space.Gameplay.Bullets;
using Ctrl_Space.Gameplay.Weapon;
using Ctrl_Space.Graphics;
using Ctrl_Space.Helpers;
using Ctrl_Space.Input;
using Ctrl_Space.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Gameplay
{
    class Ship : GameObject
    {
        private World _world;

        protected WeaponBase _weapon;
        protected WeaponBase _weaponAlt;

        private float _acceleration = 0f;

        public Ship()
            : base()
        {
        }

        public void Reset(Vector2 position, World world)
        {
            DrawHP = true;
            _world = world;

            _weapon = new PlasmaGun(this);
            _weaponAlt = new RocketLauncher(this);

            Mass = 20f;
            Position = position;
            Size = 48;
        }

        public void Rotate(float rotationSpeed)
        {
            Rotation += rotationSpeed;
        }

        public void Strafe(float strafeStep)
        {
            Speed.X += strafeStep * Maf.Cos(Rotation);
            Speed.Y += strafeStep * Maf.Sin(Rotation);
        }

        public void SpeedUp(float acceleration)
        {
            _acceleration = acceleration;
            Speed.X += acceleration * Maf.Sin(Rotation);
            Speed.Y -= acceleration * Maf.Cos(Rotation);
        }

        public void Shoot(InputDigitalState state)
        {
            if (state == InputDigitalState.Pressed)
                _weapon.On();
            else
                _weapon.Off();
        }

        public void ShootAlt(InputDigitalState state)
        {
            if (state == InputDigitalState.Pressed)
                _weaponAlt.On();
            else
                _weaponAlt.Off();
        }

        public override void Update(World world, Particles particles)
        {
            _weapon.Update(_world);
            _weaponAlt.Update(_world);
            foreach (var col in Collisions)
                Collided(col, world, particles);
            if (_acceleration > 0)
                particles.Emit(ParticleManager.EngineFire, Position - new Vector2(10f * Maf.Sin(Rotation), -10f * Maf.Cos(Rotation)), Speed - _acceleration * new Vector2(8f * Maf.Sin(Rotation), -8f * Maf.Cos(Rotation)) + Chaos.GetFloat() * Chaos.GetVector2());
            base.Update(world, particles);
            Speed *= .99f;
        }

        private void Collided(Collision col, World world, Particles particles)
        {
            if (col.GameObject is SpeedBonus)
            {
                Speed += new Vector2(10f * Maf.Sin(Rotation), -10f * Maf.Cos(Rotation));
            }

            if (col.GameObject is PlasmaBullet)
                HP -= 3;
            if (col.GameObject is Rocket)
                HP -= 7;
            if (HP <= 0)
            {
                var t = this.GetType();
                Game.DebugConsole.Append(t == typeof(EnemyShip) ? "Player win!" : "CPU win!").NewLine();
                for (int i = 0; i < _world.Count; i++)
                {
                    var ship = _world[i] as Ship;
                    if (ship != null)
                        ship.HP = ship.MaxHP;
                }
            }
        }

        public override Texture2D GetTexture()
        {
            return TextureManager.ShipOffTexture;
        }
    }
}
