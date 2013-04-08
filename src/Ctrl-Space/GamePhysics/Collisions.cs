using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Ctrl_Space
{
    class Collisions
    {
        public static void Detect(List<GameObject> gameObjects, Particles particles, ParticleParameters particleParameters)
        {
            for (int j = 0; j < gameObjects.Count; j++)
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    if (i == j || i < 0 || j < 0) continue;
                    var go1 = gameObjects[i];
                    var go2 = gameObjects[j];
                    float dx = go2.Position.X - go1.Position.X;
                    float dy = go2.Position.Y - go1.Position.Y;
                    float rr = go2.Size / 2 + go1.Size / 2;
                    float ol2 = rr * rr - dx * dx - dy * dy;
                    if (ol2 > 0)
                    {
                        if ((go1 is RocketWeapon || go1 is PlasmaBullet) && (go2 is Asteroid || go2 is Ship))
                        {
                            for(int h = 0; h < 100; h++)
                                particles.Emit(particleParameters, (go1.Position + go2.Position) / 2f, 3f * Chaos.GetFloat() * Chaos.GetVector2());
                            gameObjects.RemoveAt(i--);
                            if (go2 is Asteroid)
                            {
                                go2.Size -= 20f;
                                if (go2.Size < 40f)
                                    gameObjects.RemoveAt(j--);
                            }
                            continue;
                        }

                        if ((go1 is Ship) && go2 is SpeedBonus)
                        {
                            go1.Speed += new Vector2(10f * Maf.Sin(go1.Rotation), -10f * Maf.Cos(go1.Rotation));
                            gameObjects.RemoveAt(j--);
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
