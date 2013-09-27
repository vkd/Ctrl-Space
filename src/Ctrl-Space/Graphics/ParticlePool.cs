using System.Collections.Concurrent;

namespace Ctrl_Space.Graphics
{
    class ParticlePool
    {
        private ConcurrentBag<Particle> _particles;

        public ParticlePool()
        {
            _particles = new ConcurrentBag<Particle>();
        }

        public Particle GetParticle(ParticleParameters particleParameters)
        {
            Particle particle;
            if (_particles.TryTake(out particle))
            {
                particle.Reset(particleParameters);
                return particle;
            }
            return new Particle(particleParameters);
        }

        public void PutParticle(Particle particle)
        {
            _particles.Add(particle);
        }
    }
}
