using Ctrl_Space.Input;

namespace Ctrl_Space.GameClasses.Weapon
{
    abstract class WeaponBase
    {
        private readonly GameObject _owner;

        public WeaponBase(GameObject owner)
        {
            _owner = owner;
        }

        public GameObject Owner { get { return _owner; } }

        // implementation of hold-to-shoot
        // maybe must be moved to IntervalWeaponBase or smth.
        protected int _shootingInterval = 50;
        private int _currentInterval = 0;
        private bool _fire = false;
        private bool _fireOnce = false;

        public void On()
        {
            _fire = true;
            _fireOnce = true;
        }

        public void Off()
        {
            _fire = false;
        }

        public void Update(World world)
        {
            if ((_fire || _fireOnce) && _currentInterval == 0)
            {
                Shoot(world);
                _fireOnce = false;
            }
            if (_fire || _currentInterval != 0)
                _currentInterval++;
            if (_currentInterval > _shootingInterval)
                _currentInterval = 0;
        }

        public abstract void Shoot(World world);
    }
}
