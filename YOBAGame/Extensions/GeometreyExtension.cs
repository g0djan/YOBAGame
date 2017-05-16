using System;
using System.Data.Common;
using System.Drawing;
using Archimedes.Geometry;

namespace YOBAGame.Extensions
{
    public static class GeometreyExtension
    {
        public static double PseudoVectMul(this Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static PointF ToLocation(this Vector2 vector)
        {
            return new PointF((float)vector.X, (float)vector.Y);
        }

        public static double GetRadiansVector2Angle(this Vector2 vector)
        {
            return Math.Atan2(vector.Y, vector.X);
        }
    }
}