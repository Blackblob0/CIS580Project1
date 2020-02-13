using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public static class Collision
    {
        // CollisionCircle methods
        public static bool CollidesWith(this CollisionCircle c, CollisionCircle other)
        {
            return c.Active && other.Active && Math.Pow(other.Radius + c.Radius, 2) < Math.Pow(other.Center.X - c.Center.X, 2) + Math.Pow(other.Center.Y - c.Center.Y, 2);
        }

        public static bool CollidesWith(this CollisionCircle c, CollisionRectangle r)
        {
            float clampedX = Math.Max(Math.Min(c.Center.X, r.X + r.Width), r.X);
            float clampedY = Math.Max(Math.Min(c.Center.Y, r.Y + r.Height), r.Y);
            return c.Active && r.Active && Math.Pow(c.Radius, 2) > Math.Pow(clampedX - c.Center.X, 2) + Math.Pow(clampedY - c.Center.Y, 2);
        }

        public static bool CollidesWith(this CollisionCircle c, Vector2 v)
        {
            return c.Active && Math.Pow(c.Radius, 2) > Math.Pow(c.Center.X - v.X, 2) + Math.Pow(c.Center.Y - v.Y, 2);
        }
        // Collision Rectangle methods
        public static bool CollidesWith(this CollisionRectangle r, CollisionRectangle other)
        {
            return r.Active && other.Active && !(
                r.X + r.Width < other.X ||
                r.X > other.X + other.Width ||
                r.Y + r.Height < other.Y ||
                r.Y > other.Y + other.Height
                );
        }

        public static bool CollidesWith(this CollisionRectangle r, CollisionCircle other)
        {
            return other.CollidesWith(r);
        }

        public static bool CollidesWith(this CollisionRectangle r, Vector2 v)
        {
            return r.Active && (
                r.X < v.X &&
                r.X + r.Width > v.X &&
                r.Y < v.Y &&
                r.Y + r.Height > v.Y
                );
        }
        // Vector2 methods
        public static bool CollidesWith(this Vector2 v, Vector2 other)
        {
            return v == other;
        }

        public static bool CollidesWith(this Vector2 v, CollisionRectangle other)
        {
            return other.CollidesWith(v);
        }

        public static bool CollidesWith(this Vector2 v, CollisionCircle other)
        {
            return other.CollidesWith(v);
        }
    }
}
