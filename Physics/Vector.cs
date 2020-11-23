using System;
using Microsoft.Xna.Framework;

namespace Breakout
{
    public static class Vector {
        public static Vector2 Normalize(Vector2 vector) {
            var magnitude = Magnitude(vector);
            var x = vector.X / magnitude;
            var y = vector.Y / magnitude;

            return new Vector2(x, y);
        }
        public static float Magnitude(Vector2 vector) {
            return (float)Math.Sqrt((vector.X * vector.X) + (vector.Y * vector.Y));
        }
        public static float DotProduct(Vector2 u, Vector2 v) {
            return (u.X * v.X) + (u.Y + v.Y);
        }
        public static float AngleBetween(Vector2 u, Vector2 v) {
            var radians = Math.Acos(DotProduct(u, v) / (Magnitude(u) * Magnitude(v)));
            return (float)(radians * (180 / Math.PI));
        }
    }
}
