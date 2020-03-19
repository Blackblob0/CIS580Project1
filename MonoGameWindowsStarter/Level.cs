using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter {
    class Level {
        public Wall[] Walls;
        public Coin[] Coins;

        public Game1 game = null;

        public Vector2 InitialPlayerPosition;

        public Level(Vector2 InitialPlayerPosition) {
            this.InitialPlayerPosition = InitialPlayerPosition;
        }
        public void SetGame(Game1 game) {
            this.game = game;
            foreach (Coin coin in Coins) coin.game = game;
            foreach (Wall wall in Walls) wall.game = game;
        }
        public void LoadLevel() {
            if (game == null) throw new NullReferenceException("Call SetGame() before trying to load the level.");

            game.Walls.Clear();
            game.Coins.Clear();

            foreach (Wall wall in Walls) game.Walls.Add(wall);
            foreach (Coin coin in Coins) {
                game.Coins.Add(coin);
                coin.State.SwitchState(coin.ActiveState);
            }

            game.player.startingPosition = InitialPlayerPosition;
            game.player.Collider.X = InitialPlayerPosition.X;
            game.player.Collider.Y = InitialPlayerPosition.Y;

            game.player.InteractibleObjectsCollisionAxis.Clear();
            GameObject.GameObjectIterator(game.player.InteractibleObjects, (obj) => {
                game.player.InteractibleObjectsCollisionAxis.AddGameObject(obj);
            });

        }
    }
}
