using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Geostorm.Core
{
    static class Directions
    {
        public static readonly Vector2[] Dir =
        {
            new Vector2(1,0),
            new Vector2(0,-1),
            new Vector2(-1,0),
            new Vector2(0,1),
        };
    }

    static class MathHelper
    {
        public static float CutFloat(float value, float lowerBound, float upperBound)
        {
            if (value > upperBound) return upperBound;
            if (value < lowerBound) return lowerBound;
            return value;
        }

        public static float ToRadians(float value)
        {
            return value / 180 * MathF.PI;
        }

        public static float ToDegrees(float value)
        {
            return value * 180 / MathF.PI;
        }

        public static float ModuloFloat(float value, float lowerBound, float upperBound)
        {
            value -= lowerBound;
            value = value % (upperBound - lowerBound);
            if (value < 0) value += upperBound - lowerBound;
            value = value + lowerBound;
            return value;
        }

        public static bool GetRotation(Vector2 vector, ref float rotationOut)
        {
            if (vector.LengthSquared() != 0.0f)
            {
                if (vector.X == 0.0f)
                {
                    rotationOut = (vector.Y > 0) ? 90.0f : -90.0f;
                }
                else
                {
                    rotationOut = ToDegrees(MathF.Atan(vector.Y / vector.X));
                    if (vector.X < 0) rotationOut -= 180.0f;
                }
                rotationOut = rotationOut % 360.0f;
                return true;
            }
            return false;
        }

        public static Vector2 GetVectorRot(float rotation)
        {
            rotation = ToRadians(rotation);
            return new Vector2(MathF.Cos(rotation),MathF.Sin(rotation));
        }

        public static int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }
    }
}
