using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections;

namespace MonoGameWindowsStarter {
    public class Player : GameObject {
        Texture2D texturePlayer;
        ArrayList[] InteractibleObjects;

        public Vector2 startingPosition;
        public Vector2 playerVelocity = Vector2.Zero;

        SoundEffect sound_player_jump;

        float moveAccel = 0.3f;
        float moveSpeed = 10;
        float jumpSpeed = 25;

        public Player(Game1 game, float X, float Y, float Width, float Height) : base(game, X, Y, Width, Height) {
            startingPosition = new Vector2(X, Y);
            InteractibleObjects = new ArrayList[] { game.Walls, game.Coins };
            game.player = this;
        }

        public override void LoadContent() {
            texturePlayer = game.Content.Load<Texture2D>("Sprites/Player");
            sound_player_jump = game.Content.Load<SoundEffect>("SFX/Player-Jump");
        }

        public override void Update() {
            if (game.newKeyboardState.IsKeyDown(Keys.Right) && playerVelocity.X < moveSpeed)
                playerVelocity.X += moveAccel;

            if (game.newKeyboardState.IsKeyDown(Keys.Left) && playerVelocity.X > -moveSpeed)
                playerVelocity.X -= moveAccel;

            playerVelocity.Y -= game.gravity;

            // Move the player
            if (playerVelocity != Vector2.Zero) {
                CollisionR.X += playerVelocity.X;
                CollisionR.Y -= playerVelocity.Y;

                //Check for Collisions
                GameObjectIterator(InteractibleObjects, (obj) => {
                    switch (obj) {
                        case Wall wall:
                            if (CollisionR.CollidesWith(wall.CollisionR)) {
                                playerVelocity.Y = 0;
                                CollisionR.Y = wall.CollisionR.Y - CollisionR.Height;

                                if (game.newKeyboardState.IsKeyDown(Keys.Up)) {
                                    playerVelocity.Y += jumpSpeed;
                                    sound_player_jump.Play();
                                }
                                if (game.newKeyboardState.IsKeyUp(Keys.Right) && playerVelocity.X > 0)
                                    if (playerVelocity.X > moveAccel)
                                        playerVelocity.X -= moveAccel;
                                    else
                                        playerVelocity.X = 0;

                                if (game.newKeyboardState.IsKeyUp(Keys.Left) && playerVelocity.X < 0)
                                    if (playerVelocity.X < moveAccel)
                                        playerVelocity.X += moveAccel;
                                    else
                                        playerVelocity.X = 0;
                            }
                            break;
                        case Coin coin:
                            if (CollisionR.CollidesWith(coin.CollisionC)) {
                                coin.Collect();
                            }
                            break;
                    }
                });
            }

            // Reset Player on KeyPress V
            if (game.newKeyboardState.IsKeyDown(Keys.V) && game.oldKeyboardState.IsKeyUp(Keys.V)) {
                CollisionR.X = startingPosition.X;
                CollisionR.Y = startingPosition.Y;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool showHitBoxes) {
            base.Draw(gameTime, spriteBatch, showHitBoxes, Color.Green);
            spriteBatch.Draw(texturePlayer, CollisionR, Color.Black);

        }
    }
}
