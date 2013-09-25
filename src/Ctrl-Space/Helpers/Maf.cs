using System;

namespace Ctrl_Space.Helpers
{
    // Math for float
    static class Maf
    {
        public static float Sin(float a)
        {
            return (float)Math.Sin(a);
        }

        public static float Cos(float a)
        {
            return (float)Math.Cos(a);
        }

        public static float Sqrt(float d)
        {
            return (float)Math.Sqrt(d);
        }

        public static float Atan2(float y, float x)
        {
            return (float)Math.Atan2(y, x);
        }
    }
}
