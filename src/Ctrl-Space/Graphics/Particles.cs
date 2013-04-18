using System.Collections.Generic;
using Ctrl_Space.Gameplay;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.Graphics
{
    class Particles
    {
        private List<GameObject> _particles = new List<GameObject>();

        public void Emit(ParticleParameters particleParameters, Vector2 position, Vector2 speed)
        {
            _particles.Add(new Particle(particleParameters) { Position = position, Speed = speed });
        }

        public void Update(World world, Particles particles)
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                _particles[i].Update(world, particles);
                if ((_particles[i]).IsDestroyed)
                    _particles[i] = null;
            }
            _particles.RemoveAll(p => p == null);
        }

        public List<GameObject> ParticlesList { get { return _particles; } }
    }
}
