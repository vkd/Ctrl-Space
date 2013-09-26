using Ctrl_Space.Gameplay;
using Ctrl_Space.Helpers;
using Microsoft.Xna.Framework.Graphics;

namespace Ctrl_Space.Graphics
{
    class Particle : GameObject
    {
        ParticleParameters _particleParameters;

        private float _state = 0f;
        private float _step = .1f;

        public Particle(ParticleParameters particleParameters)
        {
            Reset(particleParameters);
        }

        public void Reset(ParticleParameters particleParameters)
        {
            IsDestroyed = false;
            _particleParameters = particleParameters;
            _step = 1f / _particleParameters.Duration;
            _state = 0f;
        }

        public override Texture2D GetTexture()
        {
            return _particleParameters.TextureGetter();
        }

        public override void Update(World world, Particles particles)
        {
            base.Update(world, particles);
            if (_state >= 1f) { IsDestroyed = true; return; }
            Size = Lerp.OfFloat(_particleParameters.Sizes, _state * (_particleParameters.Sizes.Length - 1));
            Color = Lerp.OfColor(_particleParameters.Colors, _state * (_particleParameters.Colors.Length - 1));
            Alpha = Lerp.OfFloat(_particleParameters.Alphas, _state * (_particleParameters.Alphas.Length - 1));
            _state += _step;
        }
    }
}
