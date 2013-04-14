using System.Collections.Generic;
using Ctrl_Space.GameClasses.Bullets;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class Collisions
    {
        public static void Detect(List<GameObject>[,] clusters, List<GameObject> gameObjects, Particles particles, ParticleParameters particleParameters)
        {
            int ww = clusters.GetLength(0);
            int wh = clusters.GetLength(1);

            // обход по кластерам
            for(int wj = 0; wj < wh; wj++)
                for (int wi = 0; wi < ww; wi++)
                {
                    // обход по окресности кластера 3x3
                    for(int dj = -1; dj <= 1; dj++)
                        for (int di = -1; di <= 1; di++)
                        {
                            int clui = (wi + di + ww) % ww;
                            int cluj = (wj + dj + wh) % wh;
                            // обсчёт столкновений 9 кластеров
                            for (int j = 0; j < clusters[wj, wi].Count; j++)
                                for (int i = 0; i < clusters[cluj, clui].Count; i++)
                                {
                                    if (i == j || i < 0 || j < 0) continue;
                                    var go1 = clusters[cluj, clui][i];
                                    var go2 = clusters[wj, wi][j];
                                    float dx = go2.Position.X - go1.Position.X;
                                    float dy = go2.Position.Y - go1.Position.Y;
                                    float dv = go2.Speed.X - go1.Speed.X;
                                    float du = go2.Speed.Y - go1.Speed.Y;
                                    float rr = go2.Size / 2 + go1.Size / 2;
                                    float a = dv * dv + du * du;
                                    float b = 2f * (dv * dx + du * dy);
                                    float c = dx * dx + dy * dy - rr * rr;
                                    float disc = b * b - 4f * a * c;
                                    if (disc < 0)
                                        continue;
                                    float t1 = (-b + Maf.Sqrt(disc)) / (2f * a);
                                    float t2 = (-b - Maf.Sqrt(disc)) / (2f * a);

                                    float ol2 = rr * rr - dx * dx - dy * dy;
                                    //if ((t1 <= 0 && t1 > -1) || (t2 <= 0 && t2 > -1))
                                    if (ol2 > 0)
                                    {
                                        if ((go1 is Rocket || go1 is PlasmaBullet) && (go2 is Asteroid || go2 is Ship))
                                        {
                                            for (int h = 0; h < 100; h++)
                                                particles.Emit(particleParameters, (go1.Position + go2.Position) / 2f, 3f * Chaos.GetFloat() * Chaos.GetVector2());
                                            gameObjects.Remove(go1);
                                            if (go2 is Asteroid)
                                            {
                                                go2.Size -= 20f;
                                                if (go2.Size < 40f)
                                                    gameObjects.Remove(go2);
                                            }
                                            continue;
                                        }

                                        if ((go1 is Ship) && go2 is SpeedBonus)
                                        {
                                            go1.Speed += new Vector2(10f * Maf.Sin(go1.Rotation), -10f * Maf.Cos(go1.Rotation));
                                            gameObjects.Remove(go2);
                                            continue;
                                        }

                                        // ось столкновения и нормаль к ней
                                        var nrm = new Vector2(dx, dy);
                                        var tan = new Vector2(dy, -dx);
                                        nrm.Normalize();
                                        tan.Normalize();

                                        // проекция скорости на ось столкновения (нормальная скорость)
                                        float go1nrm = Vector2.Dot(go1.Speed, nrm);
                                        float go2nrm = Vector2.Dot(go2.Speed, nrm);

                                        // перераспределяем импульс между нормальными скоростями в соответствии с массами
                                        float go1rsp = ((go1.Mass - go2.Mass) * go1nrm + 2f * go2.Mass * go2nrm) / (go1.Mass + go2.Mass);
                                        float go2rsp = ((go2.Mass - go1.Mass) * go2nrm + 2f * go1.Mass * go1nrm) / (go1.Mass + go2.Mass);

                                        // проекция скорости на нормаль к оси столкновения (тангенциальная скорость)
                                        float go1tan = Vector2.Dot(go1.Speed, tan);
                                        float go2tan = Vector2.Dot(go2.Speed, tan);

                                        go1.Speed = nrm * go1rsp + tan * go1tan;
                                        go2.Speed = nrm * go2rsp + tan * go2tan;

                                        float gp = Maf.Sqrt(ol2);
                                        go1.Position -= gp * go2.Mass / (go1.Mass + go2.Mass) * nrm;
                                        go2.Position += gp * go1.Mass / (go1.Mass + go2.Mass) * nrm;
                                    }
                                }
                        }
                }
        }
    }
}
