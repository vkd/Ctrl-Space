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

        public abstract void Shoot(InputDigitalState state, World world);
    }
}
