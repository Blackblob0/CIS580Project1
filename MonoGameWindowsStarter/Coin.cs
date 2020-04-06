using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Timers;

namespace MonoGameWindowsStarter {
    public class Coin : GameObject {
        public CoinState State;
        public CoinActiveState ActiveState;
        public CoinCollectedState CollectedState;

        public static SpriteAnimation coin_spin = null;
        public static SoundEffect sound_coin_collect = null;

        public Coin(Game1 game, float X, float Y) : base(game, X, Y, 10) {
            ActiveState = new CoinActiveState(this);
            CollectedState = new CoinCollectedState(this);
            State = ActiveState;
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
            State.Update();
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) {
            base.Draw(spriteBatch, gameTime);
            State.Draw(gameTime, spriteBatch);
            
        }
    }
}
