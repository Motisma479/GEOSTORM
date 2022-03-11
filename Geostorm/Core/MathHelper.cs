﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace Geostorm.Core
{
    static class MathHelper
    {
        public static float cutFloat(float value, float lowerBound, float upperBound)
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
            value = value + lowerBound;
            return value;
        }

        public static bool getRotation(Vector2 vector, ref float rotationOut)
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

        public static Vector2 getVectorRot(float rotation)
        {
            rotation = ToRadians(rotation);
            return new Vector2(MathF.Cos(rotation),MathF.Sin(rotation));
        }
    }
}