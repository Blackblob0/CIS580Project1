using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter {
    public interface ICollider {
        bool Active { get; set; }
        float X { get; set; }
        float Y { get; set; }
        float Width { get; }
        float Height { get; }
        bool CollidesWith(ICollider other);
        Rectangle ToRectangle();
    }
}
