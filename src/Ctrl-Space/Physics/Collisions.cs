using System;
using System.Collections.Generic;
using Ctrl_Space.Gameplay;
using Ctrl_Space.Helpers;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.Physics
{
    class Collisions
    {
        public static void Detect(List<GameObject>[,] clusters, World gameObjects)
        {
            for (int k = 0; k < gameObjects.Count; k++)
                gameObjects[k].Collisions.Clear();

            int ww = clusters.GetLength(0);
            int wh = clusters.GetLength(1);

            // обход по кластерам
            for (int wj = 0; wj < wh; wj++)
                for (int wi = 0; wi < ww; wi++)
                {
                    // обход по окресности кластера 3x3
                    for (int k = 0; k < 5; k++)
                    {
                        int di = 0;
                        int dj = 0;
                        switch (k)
                        {
                            case 0: di = 0; dj = 0; break;
                            case 1: di = 1; dj = -1; break;  // . . 1
                            case 2: di = 1; dj = 0; break;   // . 0 2
                            case 3: di = 1; dj = 1; break;   // . 4 3
                            case 4: di = 0; dj = 1; break;
                        }
                        int clui = (wi + di + ww) % ww;
                        int cluj = (wj + dj + wh) % wh;
                        float fx = (wi + di - clui) * 256f; //!HARDCODE!
                        float fy = (wj + dj - cluj) * 256f; //!HARDCODE!
                        // обсчёт столкновений 9 кластеров
                        for (int j = 0; j < clusters[cluj, clui].Count; j++)
                            for (int i = k == 0 ? j : 0; i < clusters[wj, wi].Count; i++)
                            {
                                if (cluj == wj && clui == wi && i == j)  // skip self
                                    continue;
                                var go1 = clusters[cluj, clui][j];
                                var go2 = clusters[wj, wi][i];
                                float dx = go2.Position.X - go1.Position.X - fx;
                                float dy = go2.Position.Y - go1.Position.Y - fy;
                                float dv = go2.Speed.X - go1.Speed.X;
                                float du = go2.Speed.Y - go1.Speed.Y;
                                float rr = go2.Size / 2 + go1.Size / 2;
                                if (dv == 0f || du == 0f)
                                {
                                    float ol2 = rr * rr - dx * dx - dy * dy;
                                    if (ol2 > 0)
                                    {
                                        go1.Collisions.Add(new Collision { GameObject = go2, DepthSquared = ol2, Time = 0f, Delta = new Vector2(dx, dy) });
                                        go2.Collisions.Add(new Collision { GameObject = go1, DepthSquared = ol2, Time = 0f, Delta = new Vector2(-dx, -dy) });
                                    }
                                }
                                else
                                {
                                    float a = dv * dv + du * du;
                                    float b = 2f * (dv * dx + du * dy);
                                    float c = dx * dx + dy * dy - rr * rr;
                                    float disc = b * b - 4f * a * c;
                                    if (disc < 0)
                                        continue;
                                    float t1 = (-b + Maf.Sqrt(disc)) / (2f * a);
                                    float t2 = (-b - Maf.Sqrt(disc)) / (2f * a);

                                    float min = Math.Min(t1, t2);
                                    float max = Math.Max(t1, t2);

                                    float ol2 = rr * rr - dx * dx - dy * dy;
                                    if (min < 1f && max >= 0f)
                                    {
                                        go1.Collisions.Add(new Collision { GameObject = go2, DepthSquared = ol2, Time = min, Delta = new Vector2(dx, dy) });
                                        go2.Collisions.Add(new Collision { GameObject = go1, DepthSquared = ol2, Time = min, Delta = new Vector2(-dx, -dy) });
                                    }
                                }
                            }
                    }
                }
        }
    }
}
