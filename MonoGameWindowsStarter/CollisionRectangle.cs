using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public struct CollisionRectangle : ICollider
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public bool Active { get; set; }
        public Vector2 Center
        {
            get => new Vector2(X + Width / 2, Y + Height / 2);
        }

        public CollisionRectangle(float X, float Y, float Width, float Height)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
            Active = true;
        }

        public bool CollidesWith(ICollider otherCollider) {
            switch (otherCollider) {
                case CollisionRectangle other:
                    return Active && other.Active && !(
                        X + Width < other.X ||
                        X > other.X + other.Width ||
                        Y + Height < other.Y ||
                        Y > other.Y + other.Height
                        );
                case CollisionCircle other:
                    return other.CollidesWith(this);
                case CollisionVector other:
                    return Active && (
                        X < other.Vector.X &&
                        X + Width > other.Vector.X &&
                        Y < other.Vector.Y &&
                        Y + Height > other.Vector.Y
                        );
                default:
                    return false;
            }
        }

        public Rectangle ToRectangle() {
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }
    }
}
