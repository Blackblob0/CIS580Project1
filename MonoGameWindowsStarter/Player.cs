using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace MonoGameWindowsStarter
{
    class Player : GameObject
    {
        Game1 game;
        Texture2D texturePlayer;
        public Vector2 playerVelocity = Vector2.Zero;
        public CollisionRectangle collision;
        public ArrayList interactibleObjects = new ArrayList();
        Vector2 startingPosition;

        float moveAccel = 0.3f;
        float moveSpeed = 10;
        float jumpSpeed = 25;

        public Player(Game1 game, float X, float Y, float Width, float Height)
        {
            this.game = game;
            collision = new CollisionRectangle(X, Y, Width, Height);
            startingPosition = new Vector2(X, Y);
        }

        public void LoadContent()
        {
            texturePlayer = game.Content.Load<Texture2D>("Player");
        }

        public void Update(KeyboardState oldKeyboardState, KeyboardState newKeyboardState)
        {

            if (newKeyboardState.IsKeyDown(Keys.Right) && playerVelocity.X < moveSpeed)
                playerVelocity.X += moveAccel;

            if (newKeyboardState.IsKeyDown(Keys.Left) && playerVelocity.X > -moveSpeed)
                playerVelocity.X -= moveAccel;

            playerVelocity.Y -= game.gravity;

            // Move the player
            if (playerVelocity != Vector2.Zero)
            {
                collision.X += playerVelocity.X;
                collision.Y -= playerVelocity.Y;

                //Check for Collisions
                foreach (GameObject obj in interactibleObjects)
                {
                    if (obj is Wall)
                    {
                        Wall wall = (Wall)obj;

                        if (collision.CollidesWith(wall.collision))
                        {
                            playerVelocity.Y = 0;
                            collision.Y = wall.collision.Y - collision.Height;

                            if (newKeyboardState.IsKeyDown(Keys.Up))
                                playerVelocity.Y += jumpSpeed;
                            if (newKeyboardState.IsKeyUp(Keys.Right) && playerVelocity.X > 0)
                                if (playerVelocity.X > moveAccel)
                                    playerVelocity.X -= moveAccel;
                                else
                                    playerVelocity.X = 0;

                            if (newKeyboardState.IsKeyUp(Keys.Left) && playerVelocity.X < 0)
                                if (playerVelocity.X < moveAccel)
                                    playerVelocity.X += moveAccel;
                                else
                                    playerVelocity.X = 0;
                        }
                    }
                    else if (obj is Coin && !((Coin)obj).collected)
                    {
                        Coin coin = (Coin)obj;
                        if (collision.CollidesWith(coin.collision))
                        {
                            coin.collected = true;
                        }
                }
                }
            }

            if (newKeyboardState.IsKeyDown(Keys.V) && oldKeyboardState.IsKeyUp(Keys.V)) {
                collision.X = startingPosition.X;
                collision.Y = startingPosition.Y;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool showHitBoxes)
        {
            if (showHitBoxes)
                spriteBatch.Draw(game.pixel, collision, new Color(Color.Green, 20));
            spriteBatch.Draw(texturePlayer, collision, Color.Black);
        }
    }
}
