﻿using System;
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
            return new Vector2(MathF.Cos(rotation), MathF.Sin(rotation));
        }

        public static int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }

        public static float GetDistanceBetween(Vector2 a, Vector2 b)
        {
            float i = a.X - b.X;
            float j = a.Y - b.Y;
            return MathF.Sqrt(i * i + j * j);

        }

        public static int GetInt(string buf, ref int index, int maxSize = 0)
        {
            if (maxSize == 0) maxSize = buf.Length;
            int value = 0;
            if (index >= maxSize) return value;
            char n = buf[index];
            bool negative = false;
            if (n == '-')
            {
                negative = true;
                index++;
                n = buf[index];
            }
            while (n >= '0' && n <= '9')
            {
                value *= 10;
                value += n - '0';
                index++;
                if (index == buf.Length) break;
                n = buf[index];
            }
            return (negative ? -value : value);
        }

        public static bool InferiorOrEqual(Vector2 a, Vector2 b)
        {
            if (a.X <= b.X && a.Y <= b.Y)
                return true;
            else
                return false;
        }
        public static bool SuperiorOrEqual(Vector2 a, Vector2 b)
        {
            if (a.X >= b.X && a.Y >= b.Y)
                return true;
            else
                return false;
        }
    }
}
