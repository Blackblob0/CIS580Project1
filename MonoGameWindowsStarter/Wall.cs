using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter {
    public class Wall : GameObject {
        public Wall(Game1 game, float X, float Y, float Width, float Height) : base(game, X, Y, Width, Height) {
            game.Walls.Add(this);
        }

        public override void LoadContent() { }

        public override void Update() { }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(game.pixel, Collider.ToRectangle(), Color.Blue);
        }

    }
}
