using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter {
    public class CollisionVector : ICollider {

        public Vector2 Vector;
        public float X {
            get { return Vector.X; }
            set { Vector.X = value; }
        }
        public float Y {
            get { return Vector.Y; }
            set { Vector.Y = value; }
        }
        public float Width => 0;
        public float Height => 0;
        public bool Active { get; set; }

        public CollisionVector (Vector2 Vector) {
            this.Vector = Vector;
        }

        public bool CollidesWith(ICollider otherCollider) {
            switch (otherCollider) {
                case CollisionVector other:
                    return Vector == other.Vector;
                case CollisionRectangle other:
                    return other.CollidesWith(this);
                case CollisionCircle other:
                    return other.CollidesWith(this);
                default:
                    return false;
            }
        }

        public static implicit operator Vector2(CollisionVector obj) {
            return obj.Vector;
        }

        public Rectangle ToRectangle() {
            return new Rectangle((int)Vector.X, (int)Vector.Y, 0, 0);
        }
    }
}
