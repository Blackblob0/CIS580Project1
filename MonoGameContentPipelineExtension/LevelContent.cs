using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameContentPipelineExtension {
    public class PlatformContent {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public PlatformContent (int X, int Y, int Width, int Height) {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
        }
    }
    public class CoinContent {
        public int X;
        public int Y;
        public CoinContent(int X, int Y) {
            this.X = X;
            this.Y = Y;
        }
    }
    public class LevelContent {
        public int InitialPlayerPositionX;
        public int InitialPlayerPositionY;
        public List<PlatformContent> Platforms = new List<PlatformContent>();
        public List<CoinContent> Coins = new List<CoinContent>();
    }
}
