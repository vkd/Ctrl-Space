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

        public void Emit(ParticleParameters particleParameters, Vector2 position, Vector2 speed)
        {
            _particles.Add(new Particle(particleParameters) { Position = position, Speed = speed });
        }

        public void Update()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].Update();
                if ((_particles[i]).IsDestroyed)
                    _particles.RemoveAt(i--);
            }
        }

        public List<GameObject> ParticlesList { get { return _particles; } }
    }

    class Particle : GameObject
    {
        ParticleParameters _particleParameters;

        private float _state = 1.0f;
        private float _step = .1f;

        public Particle(ParticleParameters particleParameters)
        {
            _particleParameters = particleParameters;
            _step = 1f / _particleParameters.Duration;
            Size = 16f;
            Color = Color.Red;
        }

        public override Microsoft.Xna.Framework.Graphics.Texture2D GetTexture()
        {
            return _particleParameters.TextureGetter();
        }

        public override void Update()
        {
            base.Update();
            if (_state <= 0f) { IsDestroyed = true; return; }
            // TODO Linear interpolated multiple states
            Size = _particleParameters.Sizes[0] * _state + _particleParameters.Sizes[1] * (1f - _state);
            Color = new Color(_particleParameters.Colors[0].ToVector3() * _state + _particleParameters.Colors[1].ToVector3() * (1f - _state));
            Alpha = _particleParameters.Alphas[0] * _state + _particleParameters.Alphas[1] * (1f - _state); ;
            _state -= _step;
        }
    }
}
