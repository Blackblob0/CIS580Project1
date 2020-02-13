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
        public CollisionType CollisionShape;
        public CollisionCircle CollisionC;
        public CollisionRectangle CollisionR;

        public GameObject(Game1 game, float X, float Y, float Radius) {
            this.game = game;
            this.CollisionShape = CollisionType.CIRCLE;
            CollisionC = new CollisionCircle(X, Y, Radius);
        }

        public GameObject(Game1 game, float X, float Y, float Width, float Height) {
            this.game = game;
            this.CollisionShape = CollisionType.RECTANGLE;
            CollisionR = new CollisionRectangle(X, Y, Width, Height);
        }

        abstract public void LoadContent();

        abstract public void Update();

        abstract public void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool showHitBoxes);
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool showHitBoxes, Color color) {
            if (showHitBoxes && CollisionShape == CollisionType.RECTANGLE ? CollisionR.Active : CollisionC.Active)
                spriteBatch.Draw(
                    CollisionShape == CollisionType.RECTANGLE ? game.pixel : game.circle,
                    CollisionShape == CollisionType.RECTANGLE ? (Rectangle)CollisionR : (Rectangle)CollisionC,
                    new Color(color, 20)
                    );
        }

        public static void GameObjectIterator(ArrayList[] GameObjectNestedList, Action<GameObject> handler) {
            foreach (ArrayList arr in GameObjectNestedList)
                foreach (GameObject obj in arr)
                    handler(obj);
        }
    }
}
