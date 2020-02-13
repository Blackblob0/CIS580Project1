using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public struct CollisionRectangle
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public bool Active;
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

        public static implicit operator Rectangle(CollisionRectangle r)
        {
            return new Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);
        }
    }
}
