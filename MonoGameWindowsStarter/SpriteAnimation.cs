using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter {
    public class SpriteAnimation {
        Sprite[] sprites;
        public int Count => sprites.Length;
        public SpriteAnimation(Sprite[] sprites) {
            this.sprites = sprites;
        }
        public Sprite this[int index] {
            get => sprites[index];
        }
    }
}
