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
    class Coin : GameObject
    {
        Game1 game;
        public CollisionCircle collision;
        Texture2D circle;

        public bool collected = false;

        public Coin(Game1 game, float X, float Y)
        {
            this.game = game;
            collision = new CollisionCircle(X, Y, 10);
        }
        public void LoadContent()
        {
            circle = game.Content.Load<Texture2D>("Circle");
        }

        public void Update(KeyboardState oldKeyboardState, KeyboardState newKeyboardState)
        {
            if (collected && newKeyboardState.IsKeyDown(Keys.C) && oldKeyboardState.IsKeyUp(Keys.C))
                collected = false;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool showHitBoxes)
        {
            if (!collected)
                spriteBatch.Draw(circle, collision, Color.Yellow);
        }
    }
}
