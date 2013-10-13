using System.Collections.Generic;
using Ctrl_Space.Graphics;

namespace Ctrl_Space.Gameplay
{
    class World : List<GameObject>
    {
        public void Update(World world, Particles particles)
        {
            for (int i = 0; i < Count; i++)
            {
                var obj = this[i];
                obj.Update(world, particles);
                if (obj.IsDestroyed)
                {
                    obj.ResetGameObject();
                    Game.Objects.ReleaseObject(obj);
                    this[i] = null;
                }
            }
            RemoveAll(o => o == null);
        }
    }
}
