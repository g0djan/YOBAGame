using Archimedes.Geometry;

namespace YOBAGame.Extensions
{
    public static class GeometeyExtension
    {
        public static double PseudoVectMul(this Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }
    }
}