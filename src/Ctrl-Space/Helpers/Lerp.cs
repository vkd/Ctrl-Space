using Microsoft.Xna.Framework;

namespace Ctrl_Space.Helpers
{
    class Lerp
    {
        public static float OfFloat(float v1, float v2, float t)
        {
            if (t <= 0f)
                return v1;
            if (t >= 1f)
                return v2;
            return (1f - t) * v1 + t * v2;
        }

        public static Color OfColor(Color v1, Color v2, float t)
        {
            if (t <= 0f)
                return v1;
            if (t >= 1f)
                return v2;
            return new Color((1f - t) * v1.ToVector4() + t * v2.ToVector4());
        }

        public static float OfFloat(float[] v, float t)
        {
            int len = v.Length;
            if (len == 0)
                return 0f;
            if (len == 1)
                return v[0];
            if(t <= 0f)
                return v[0];
            if (t >= len - 1)
                return v[len - 1];
            int i = (int)t;
            return OfFloat(v[i], v[i + 1], t - i);
        }

        public static Color OfColor(Color[] v, float t)
        {
            int len = v.Length;
            if (len == 0)
                return Color.Transparent;
            if (len == 1)
                return v[0];
            if (t <= 0f)
                return v[0];
            if (t >= len - 1)
                return v[len - 1];
            int i = (int)t;
            return OfColor(v[i], v[i + 1], t - i);
        }
    }
}
