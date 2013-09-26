using System.Collections.Generic;
using Ctrl_Space.Gameplay;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.Graphics
{
    class Particles
    {
        private ParticlePool _particlePool = new ParticlePool();
        private List<GameObject> _particles = new List<GameObject>();

        public void Emit(ParticleParameters particleParameters, Vector2 position, Vector2 speed)
        {
            var particle = _particlePool.GetParticle(particleParameters);
            particle.Position = position;
            particle.Speed = speed;
            _particles.Add(particle);
        }

        public void Update(World world, Particles particles)
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].Update(world, particles);
                if ((_particles[i]).IsDestroyed)
                {
                    _particlePool.PutParticle((Particle)_particles[i]);
                    _particles[i] = null;
                }
            }
            _particles.RemoveAll(p => p == null);
        }

        public List<GameObject> ParticlesList { get { return _particles; } }
    }
}
