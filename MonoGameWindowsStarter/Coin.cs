using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace MonoGameWindowsStarter {
    public class Coin : GameObject {
        public enum State { Active, Collected };
        public State state = State.Active;

        SoundEffect sound_coin_collect;

        Texture2D [] coin_animation = new Texture2D[42];
        int current_frame = 1;

        public Coin(Game1 game, float X, float Y) : base(game, X, Y, 10) {
            game.Coins.Add(this);
        }
        public override void LoadContent() {
            for (int i = 0; i < 42; i++)
                coin_animation[i] = game.Content.Load<Texture2D>("Sprites/Coin/CoinAnimation00" + i.ToString("D2"));
            sound_coin_collect = game.Content.Load<SoundEffect>("SFX/Coin-Collect");
        }

        public override void Update() {
            //Reset coins on Keypress C
            if (state == State.Collected && game.newKeyboardState.IsKeyDown(Keys.C) && game.oldKeyboardState.IsKeyUp(Keys.C))
                state = State.Active;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool showHitBoxes) {
            switch (state) {
                case State.Active:
                    current_frame = (current_frame + (int)gameTime.ElapsedGameTime.TotalMilliseconds / 15) % 42;
                    spriteBatch.Draw(coin_animation[current_frame], CollisionC, Color.White);
                    break;
                case State.Collected:
                    spriteBatch.DrawString(
                    game.spriteFont,
                    "500",
                    new Vector2(CollisionC.X, CollisionC.Y - 50),
                    Color.White
                    );
                    break;
            }
            
        }

        public void Collect() {
            if (state == State.Active) {
                state = State.Collected;
                sound_coin_collect.Play();
            }
        }
    }
}
