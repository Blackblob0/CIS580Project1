using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter {
    public abstract class CoinState {
        public Coin coin;
        public CoinState(Coin coin) {
            this.coin = coin;
        }
        public abstract void Update();
        public abstract void Draw(GameTime gameTime,SpriteBatch spriteBatch);
        public virtual void Enter() { coin.State = this; }
        public virtual void Exit() { }
        public void SwitchState(CoinState State) {
            Exit();
            State.Enter();
        }
    }

    public class CoinCollectedState : CoinState {
        public CoinCollectedState(Coin coin) : base(coin) { }
        public override void Update() {
            if (coin.game.newKeyboardState.IsKeyDown(Keys.C) && coin.game.oldKeyboardState.IsKeyUp(Keys.C)) {
                coin.State.SwitchState(coin.ActiveState);
            }
        }

        public override void Draw(GameTime gameTime ,SpriteBatch spriteBatch ) {
            spriteBatch.DrawString(
                coin.game.spriteFont,
                "500",
                new Vector2(coin.Collider.X, coin.Collider.Y - 50),
                Color.White
                );
        }
        public override void Enter() {
            base.Enter();
            coin.Collider.Active = false;
            Coin.sound_coin_collect.Play();
        }
    }
    public class CoinActiveState : CoinState {
        int current_frame = 1;
        public CoinActiveState(Coin coin) : base(coin) { }
        public override void Update() { }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            current_frame = (current_frame + (int)gameTime.ElapsedGameTime.TotalMilliseconds / 15) % 42;
            Coin.coin_spin[current_frame].Draw(spriteBatch, coin.Collider.ToRectangle(), Color.White);
        }
        public override void Enter() {
            base.Enter();
            coin.Collider.Active = true;
        }
    }
}
