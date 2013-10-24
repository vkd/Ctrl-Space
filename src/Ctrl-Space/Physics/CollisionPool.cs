using Ctrl_Space.Gameplay;
using Ctrl_Space.Helpers;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.Physics
{
    class CollisionPool : Pool<Collision>
    {
        private static readonly CollisionPool _instance = new CollisionPool();
        public static CollisionPool Instance
        {
            get { return _instance; }
        }

        public Collision GetObject(GameObject gameObject, float depthSquared, float time, Vector2 delta)
        {
            var collision = GetObject();
            collision.GameObject = gameObject;
            collision.DepthSquared = depthSquared;
            collision.Time = time;
            collision.Delta = delta;
            return collision;
        }
    }
}
