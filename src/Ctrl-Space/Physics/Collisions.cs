using System;
using System.Collections.Generic;
using Ctrl_Space.Physics;

namespace Ctrl_Space
{
    class Collisions
    {
        public static List<Collision> Detect(List<GameObject>[,] clusters, List<GameObject> gameObjects, Particles particles, ParticleParameters particleParameters)
        {
            List<Collision> collided = new List<Collision>();

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
                                        collided.Add(new Collision() { GameObjectA = go1, GameObjectB = go2, Depth = ol2, Time = 0f });
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

                                    float ol2 = rr * rr - dx * dx - dy * dy;
                                    if ((t1 <= 0 && t2 > 0) || (t2 <= 0 && t1 > 0))
                                    {
                                        collided.Add(new Collision() { GameObjectA = go1, GameObjectB = go2, Depth = ol2, Time = Math.Min(t1, t2) });
                                    }
                                }
                                //if (ol2 > 0)
                                //{
                                //    particles.Emit(particleParameters, ((go1.Position + new Vector2(fx, fy)) * go2.Size + go2.Position * go1.Size) / (go1.Size + go2.Size), Vector2.Zero);
                                //particles.Emit(particleParameters, go1.Position, Vector2.Zero);
                                //particles.Emit(particleParameters, go2.Position, Vector2.Zero);
                                //if ((go1 is Rocket || go1 is PlasmaBullet) && (go2 is Asteroid || go2 is Ship))
                                //{
                                //    for (int h = 0; h < 100; h++)
                                //        particles.Emit(particleParameters, (go1.Position + go2.Position) / 2f, 3f * Chaos.GetFloat() * Chaos.GetVector2());
                                //    gameObjects.Remove(go1);
                                //    if (go2 is Asteroid)
                                //    {
                                //        go2.Size -= 20f;
                                //        if (go2.Size < 40f)
                                //            gameObjects.Remove(go2);
                                //    }
                                //    continue;
                                //}

                                //if ((go1 is Ship) && go2 is SpeedBonus)
                                //{
                                //    go1.Speed += new Vector2(10f * Maf.Sin(go1.Rotation), -10f * Maf.Cos(go1.Rotation));
                                //    gameObjects.Remove(go2);
                                //    continue;
                                //}

                                //if (ol2 > 0)
                                //{

                                //    // ось столкновения и нормаль к ней
                                //    var nrm = new Vector2(dx, dy);
                                //    var tan = new Vector2(dy, -dx);
                                //    nrm.Normalize();
                                //    tan.Normalize();

                                //    // проекция скорости на ось столкновения (нормальная скорость)
                                //    float go1nrm = Vector2.Dot(go1.Speed, nrm);
                                //    float go2nrm = Vector2.Dot(go2.Speed, nrm);

                                //    // перераспределяем импульс между нормальными скоростями в соответствии с массами
                                //    float go1rsp = ((go1.Mass - go2.Mass) * go1nrm + 2f * go2.Mass * go2nrm) / (go1.Mass + go2.Mass);
                                //    float go2rsp = ((go2.Mass - go1.Mass) * go2nrm + 2f * go1.Mass * go1nrm) / (go1.Mass + go2.Mass);

                                //    // проекция скорости на нормаль к оси столкновения (тангенциальная скорость)
                                //    float go1tan = Vector2.Dot(go1.Speed, tan);
                                //    float go2tan = Vector2.Dot(go2.Speed, tan);

                                //    go1.Speed = nrm * go1rsp + tan * go1tan;
                                //    go2.Speed = nrm * go2rsp + tan * go2tan;

                                //    float gp = Maf.Sqrt(ol2);
                                //    go1.Position -= gp * go2.Mass / (go1.Mass + go2.Mass) * nrm;
                                //    go2.Position += gp * go1.Mass / (go1.Mass + go2.Mass) * nrm;
                                //}
                                //}
                            }
                    }
                }
            return collided;
        }
    }
}
