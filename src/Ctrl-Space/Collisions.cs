using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class Collisions
    {
        public static void Detect(List<GameObject> gameObjects)
        {
            for (int j = 0; j < gameObjects.Count; j++)
                for (int i = j + 1; i < gameObjects.Count; i++)
                {
                    var go1 = gameObjects[i];
                    var go2 = gameObjects[j];
                    float dx = go2.Position.X - go1.Position.X;
                    float dy = go2.Position.Y - go1.Position.Y;
                    float rr = go2.Size / 2 + go1.Size / 2;
                    float ol2 = rr * rr - dx * dx - dy * dy;
                    if (ol2 > 0)
                    {
                        var vel = (go2.Speed - go1.Speed).Length();
                        var dir = new Vector2(dx, dy);
                        dir.Normalize();
                        go1.Speed = (-dir * vel) * go2.Mass / (go1.Mass + go2.Mass);
                        go2.Speed = (dir * vel) * go1.Mass / (go1.Mass + go2.Mass);
                    }
                }
        }
    }
}
