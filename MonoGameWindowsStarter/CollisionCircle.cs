using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public struct CollisionCircle : ICollider
    {
        public float X { get => Center.X - Radius; set => Center.X = value - Radius; }
        public float Y { get => Center.Y - Radius; set => Center.Y = value - Radius; }
        public float Width => Radius * 2;
        public float Height => Radius * 2;
        public float Radius;
        public bool Active { get; set; }
        public Vector2 Center;

        public CollisionCircle(float X, float Y, float Radius)
        {
            this.Center = new Vector2(X - Radius, Y - Radius);
            this.Radius = Radius;
            Active = true;
        }

        // CollisionCircle methods
        public bool CollidesWith(ICollider otherCollider) {
            switch (otherCollider) {
                case CollisionCircle other:
                    return Active && other.Active && Math.Pow(other.Radius + Radius, 2) < Math.Pow(other.Center.X - Center.X, 2) + Math.Pow(other.Center.Y - Center.Y, 2);
                case CollisionRectangle other:
                    float clampedX = Math.Max(Math.Min(Center.X, other.X + other.Width), other.X);
                    float clampedY = Math.Max(Math.Min(Center.Y, other.Y + other.Height), other.Y);
                    return Active && other.Active && Math.Pow(Radius, 2) > Math.Pow(clampedX - Center.X, 2) + Math.Pow(clampedY - Center.Y, 2);
                case CollisionVector other:
                    return Active && Math.Pow(Radius, 2) > Math.Pow(Center.X - other.Vector.X, 2) + Math.Pow(Center.Y - other.Vector.Y, 2);
                default:
                    return false;
            }
            
        }

        public Rectangle ToRectangle() {
            return new Rectangle((int)X, (int)Y, (int)Radius * 2, (int)Radius * 2);
        }
    }
}
