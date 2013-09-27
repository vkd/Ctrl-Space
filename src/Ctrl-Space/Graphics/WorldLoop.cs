using System;
using System.Collections.Generic;
using Ctrl_Space.Gameplay;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.Graphics
{
    class WorldLoop
    {
        private List<GameObject>[,] _clusters = new List<GameObject>[Game.WorldHeihgtInClusters, Game.WorldWidthInClusters];

        public WorldLoop()
        {
            for (int j = 0; j < Game.WorldHeihgtInClusters; j++)
            {
                for (int i = 0; i < Game.WorldWidthInClusters; i++)
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

        public void Clusterize(params IList<GameObject>[] gameObjects)
        {
            for (int j = 0; j < Game.WorldHeihgtInClusters; j++)
            {
                for (int i = 0; i < Game.WorldWidthInClusters; i++)
                {
                    _clusters[j, i].Clear();
                }
            }

            foreach (var gameObject in gameObjects)
            {
                foreach (var go in gameObject)
                {
                    int i = (int)(((long)go.Position.X) >> Game.ClusterSizeInPowerOfTwo);
                    int j = (int)(((long)go.Position.Y) >> Game.ClusterSizeInPowerOfTwo);
                    i = (i + Game.WorldWidthInClusters) % Game.WorldWidthInClusters;
                    j = (j + Game.WorldHeihgtInClusters) % Game.WorldHeihgtInClusters;
                    _clusters[j, i].Add(go);
                }
            }
        }

        private List<Cluster> _getClustersAroundPositionResult = new List<Cluster>();

        // simple AABB approach
        public List<Cluster> GetClustersAroundPosition(Vector2 position, float distance)
        {
            _getClustersAroundPositionResult.Clear();
            int i1 = (int)Math.Floor((position.X - distance) / Game.ClusterSize);
            int j1 = (int)Math.Floor((position.Y - distance) / Game.ClusterSize);
            int i2 = (int)Math.Floor((position.X + distance) / Game.ClusterSize);
            int j2 = (int)Math.Floor((position.Y + distance) / Game.ClusterSize);

            for (int j = j1; j <= j2; j++)
            {
                for (int i = i1; i <= i2; i++)
                {
                    Cluster cluster = new Cluster();
                    cluster.ShiftX = i < 0 ? ((i - Game.WorldWidthInClusters + 1) / Game.WorldWidthInClusters) : (i / Game.WorldWidthInClusters);
                    cluster.ShiftY = j < 0 ? ((j - Game.WorldHeihgtInClusters + 1) / Game.WorldHeihgtInClusters) : (j / Game.WorldHeihgtInClusters);
                    cluster.GameObjects = _clusters[j - cluster.ShiftY * Game.WorldHeihgtInClusters, i - cluster.ShiftX * Game.WorldWidthInClusters];
                    _getClustersAroundPositionResult.Add(cluster);
                }
            }

            return _getClustersAroundPositionResult;
        }
    }

    class Cluster
    {
        public int ShiftX;
        public int ShiftY;
        public List<GameObject> GameObjects;
    }
}
