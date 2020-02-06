using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter
{
    class Wall : GameObject
    {
        Game1 game;
        public CollisionRectangle collision;

        public Wall(Game1 game, float X, float Y, float Width, float Height)
        {
            this.game = game;
            collision = new CollisionRectangle(X, Y, Width, Height);
        }

        public void LoadContent()
        {
        }

        public void Update(KeyboardState oldKeyboardState, KeyboardState newKeyboardState)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool showHitBoxes)
        {

            spriteBatch.Draw(game.pixel, collision, Color.Red);
        }
    }
}
