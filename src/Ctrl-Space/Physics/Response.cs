using Ctrl_Space.Gameplay;
using Microsoft.Xna.Framework;

namespace Ctrl_Space.Physics
{
    class Response
    {
        public static void Apply(GameObject go, Collision collision)
        {
            var go1 = go;
            var go2 = collision.GameObject;

            // ось столкновения и нормаль к ней
            var nrm = collision.Delta;
            var tan = new Vector2(collision.Delta.Y, -collision.Delta.X);
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

            //float gp = Maf.Sqrt(collision.DepthSquared);
            //go1.Position -= gp * go2.Mass / (go1.Mass + go2.Mass) * nrm;
            //go2.Position += gp * go1.Mass / (go1.Mass + go2.Mass) * nrm;
        }
    }
}
