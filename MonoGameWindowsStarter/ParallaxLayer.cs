using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter {
    class ParallaxLayer : DrawableGameComponent{
        public List<ISprite> Sprites = new List<ISprite>();
        public IScrollController ScrollController { get; set; }
        SpriteBatch spriteBatch;
        public ParallaxLayer(Game game) : base(game) {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }
        public override void Draw(GameTime gameTime) {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, ScrollController.Transform);
            foreach (var sprite in Sprites) {
                sprite.Draw(spriteBatch, gameTime);
            }
            spriteBatch.End();
        }
        public override void Update(GameTime gameTime) {
            ScrollController.Update(gameTime);
        }
    }
}
