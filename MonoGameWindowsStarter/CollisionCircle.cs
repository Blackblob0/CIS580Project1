using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public struct CollisionCircle
    {
        public float X;
        public float Y;
        public float Radius;
        public bool Active;
        public Vector2 Center
        {
            get => new Vector2(X + Radius, Y + Radius);
        }

        public CollisionCircle(float X, float Y, float Radius)
        {
            this.X = X;
            this.Y = Y;
            this.Radius = Radius;
            Active = true;
        }

        public static implicit operator Rectangle(CollisionCircle c)
        {
            return new Rectangle((int)c.X, (int)c.Y, (int)c.Radius * 2, (int)c.Radius * 2);
        }
    }
}
