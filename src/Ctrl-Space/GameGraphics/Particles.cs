using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class Particles
    {
        private List<GameObject> _particles = new List<GameObject>();

        public void Emit(Vector2 position, Vector2 speed)
        {
            _particles.Add(new Particle() { Position = position, Speed = speed });
        }

        public void Update()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].Update();
                if (((Particle)_particles[i]).IsDestroyed)
                    _particles.RemoveAt(i--);
            }
        }

        public List<GameObject> ParticlesList { get { return _particles; } }
    }

    class Particle : GameObject
    {
        public bool IsDestroyed = false;

        private float _state = 1.0f;

        public Particle()
        {
            Size = 16f;
            Color = Color.Red;
        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D GetTexture()
        {
            return TextureManager.SimpleGlowTexture;
        }

        public override void Update()
        {
            base.Update();
            if (_state <= 0f) { IsDestroyed = true; return; }
            Size = 32f * _state;
            Color = new Color(1f, .7f * _state, 0f);
            Alpha = _state;
            _state -= .05f;
        }
    }
}
