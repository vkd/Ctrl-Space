using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class World : List<GameObject>
    {
        // hardcode
        private List<GameObject>[,] _clusters = new List<GameObject>[8, 8];

        public World()
        {
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    _clusters[j, i] = new List<GameObject>();
                }
            }
        }

        public List<GameObject>[,] Clusters
        {
            get
            {
                return _clusters;
            }
        }

        public void Clusterize()
        {
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    _clusters[j, i].Clear();
                }
            }

            foreach (var go in this)
            {
                int i = (int)(((long)go.Position.X) >> 8);
                int j = (int)(((long)go.Position.Y) >> 8);
                i = (i + 8) % 8;
                j = (j + 8) % 8;
                _clusters[j, i].Add(go);
            }
        }

        // simple AABB approach
        public List<Cluster> GetClustersAroundPosition(Vector2 position, float distance)
        {
            List<Cluster> result = new List<Cluster>();
            int i1 = (int)Math.Floor((position.X - distance) / 256);
            int j1 = (int)Math.Floor((position.Y - distance) / 256);
            int i2 = (int)Math.Floor((position.X + distance) / 256);
            int j2 = (int)Math.Floor((position.Y + distance) / 256);

            for (int j = j1; j <= j2; j++)
            {
                for (int i = i1; i <= i2; i++)
                {
                    Cluster cluster = new Cluster();
                    cluster.ShiftX = (i + 65536) / 8 - 8192; // TODO hackaway
                    cluster.ShiftY = (j + 65536) / 8 - 8192; // TODO hackaway
                    cluster.GameObjects = _clusters[(j + 65536) % 8, (i + 65536) % 8]; // TODO hackaway
                    result.Add(cluster);
                }
            }

            return result;
        }
    }

    public class Cluster
    {
        public int ShiftX;
        public int ShiftY;
        public List<GameObject> GameObjects;
    }
}
