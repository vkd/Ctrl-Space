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

        private int _clusterLeft;
        private int _clusterRight;
        private int _clusterTop;
        private int _clusterBottom;
        private int _clusterX;
        private int _clusterY;
        private Cluster _cluster = new Cluster();

        public void PrepareClustersForRender(Vector2 position, float distance)
        {
            _clusterLeft = (int)Math.Floor((position.X - distance) / Game.ClusterSize);
            _clusterBottom = (int)Math.Floor((position.Y - distance) / Game.ClusterSize);
            _clusterRight = (int)Math.Floor((position.X + distance) / Game.ClusterSize);
            _clusterTop = (int)Math.Floor((position.Y + distance) / Game.ClusterSize);
            _clusterX = _clusterLeft;
            _clusterY = _clusterBottom;
        }

        public bool FetchClustersForRender()
        {
            if (_clusterX > _clusterRight || _clusterY > _clusterTop)
                return false;

            _cluster.ShiftX = _clusterX < 0 ? ((_clusterX - Game.WorldWidthInClusters + 1) / Game.WorldWidthInClusters) : (_clusterX / Game.WorldWidthInClusters);
            _cluster.ShiftY = _clusterY < 0 ? ((_clusterY - Game.WorldHeihgtInClusters + 1) / Game.WorldHeihgtInClusters) : (_clusterY / Game.WorldHeihgtInClusters);
            _cluster.GameObjects = _clusters[_clusterY - _cluster.ShiftY * Game.WorldHeihgtInClusters, _clusterX - _cluster.ShiftX * Game.WorldWidthInClusters];

            _clusterX++;
            if (_clusterX > _clusterRight)
            {
                _clusterX = _clusterLeft;
                _clusterY++;
            }
            return true;
        }

        public Cluster Cluster
        {
            get { return _cluster; }
        }
    }



    class Cluster
    {
        public int ShiftX;
        public int ShiftY;
        public List<GameObject> GameObjects;
    }
}
