using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter {
    abstract public class GameObject {
        public Game1 game;
        public enum CollisionType { RECTANGLE, CIRCLE };
        public ICollider Collider;

        public GameObject(Game1 game, float X, float Y, float Radius) {
            this.game = game;
            Collider = new CollisionCircle(X, Y, Radius);
        }

        public GameObject(Game1 game, float X, float Y, float Width, float Height) {
            this.game = game;
            Collider = new CollisionRectangle(X, Y, Width, Height);
        }

        abstract public void LoadContent();

        abstract public void Update();

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

#if DEBUG
            Color color = Color.White;

            switch(this) {
                case Player _:
                    color = Color.Green;
                    break;
                case Coin _:
                    color = Color.Red;
                    break;
                case Wall _:
                    color = Color.Blue;
                    break;
            }

            if (Collider.Active)
                spriteBatch.Draw(
                    Collider.GetType() == typeof(CollisionRectangle) ? game.pixel : game.circle,
                    Collider.ToRectangle(),
                    new Color(color, 20)
                    );
#endif
        }

        public static void GameObjectIterator(ArrayList[] GameObjectNestedList, Action<GameObject> handler) {
            foreach (ArrayList arr in GameObjectNestedList)
                foreach (GameObject obj in arr)
                    handler(obj);
        }
    }
}
