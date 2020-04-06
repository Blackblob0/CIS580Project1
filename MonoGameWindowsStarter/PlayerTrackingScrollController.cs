using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter {
    class PlayerTrackingScrollController : IScrollController {
        Player player;
        public float ScrollRatio = 1.0f;
        public float Offset = 200;
        public Matrix Transform {
            get {
                float x = ScrollRatio * (Offset - player.Collider.X);
                return Matrix.CreateTranslation(x, 0, 0);
            }
        }
        public PlayerTrackingScrollController(Player player, float ratio) {
            this.player = player;
            this.ScrollRatio = ratio;
            this.Offset = player.game.GraphicsDevice.Viewport.Width;
        }
        public void Update(GameTime gameTime) { }
    }
}
