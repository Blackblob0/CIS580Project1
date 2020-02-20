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

        static SpriteAnimation coin_spin = null;
        static SoundEffect sound_coin_collect = null;

        
        int current_frame = 1;

        public Coin(Game1 game, float X, float Y) : base(game, X, Y, 10) {
            game.Coins.Add(this);
        }
        public override void LoadContent() {
            if (coin_spin == null) {
                Sprite [] coin_animation = new Sprite[42];
                for (int i = 0; i < 42; i++)
                    coin_animation[i] = new Sprite(Collider.ToRectangle(), game.Content.Load<Texture2D>("Sprites/Coin/CoinAnimation00" + i.ToString("D2")));
                coin_spin = new SpriteAnimation(coin_animation);
            }
            if (sound_coin_collect == null)
                sound_coin_collect = game.Content.Load<SoundEffect>("SFX/Coin-Collect");
        }

        public override void Update() {
            //Reset coins on Keypress C
            if (state == State.Collected && game.newKeyboardState.IsKeyDown(Keys.C) && game.oldKeyboardState.IsKeyUp(Keys.C)) {
                state = State.Active;
                Collider.Active = true;
            }
                
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            base.Draw(gameTime, spriteBatch);

            switch (state) {
                case State.Active:
                    current_frame = (current_frame + (int)gameTime.ElapsedGameTime.TotalMilliseconds / 15) % 42;
                    coin_spin[current_frame].Draw(spriteBatch, Collider.ToRectangle(), Color.White);
                    break;
                case State.Collected:
                    spriteBatch.DrawString(
                    game.spriteFont,
                    "500",
                    new Vector2(Collider.X, Collider.Y - 50),
                    Color.White
                    );
                    break;
            }
            
        }

        public void Collect() {
            if (state == State.Active) {
                Collider.Active = false;
                state = State.Collected;
                sound_coin_collect.Play();
            }
        }
    }
}
