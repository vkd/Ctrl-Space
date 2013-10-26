using System.Collections.Generic;
using Ctrl_Space.Graphics;

namespace Ctrl_Space.Gameplay
{
    class World
    {
        private List<GameObject> _gameObjects = new List<GameObject>();

        public void Add(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public int Count
        {
            get { return _gameObjects.Count; }
        }

        public GameObject this[int i]
        {
            get { return _gameObjects[i]; }
        }

        public void Update(World world, Particles particles)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                var obj = _gameObjects[i];
                obj.Update(world, particles);
                if (obj.IsDestroyed)
                {
                    obj.ResetGameObject();
                    Game.Objects.ReleaseObject(obj);
                    _gameObjects[i] = null;
                }
            }
            _gameObjects.RemoveAll(o => o == null);
        }

        public List<GameObject> GameObjects
        {
            get { return _gameObjects; }
        }
    }
}
