using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsStarter {
    public class Sprite {
        private Rectangle source;
        private Vector2 characterOffset = Vector2.Zero;
        private Texture2D texture;
        public int Width => source.Width;
        public int Height => source.Height;
        public Sprite(Rectangle source, Texture2D texture) {
            this.texture = texture;
            this.source = source;
        }
        public Sprite(Rectangle source, Texture2D texture, Vector2 characterOffset) {
            this.texture = texture;
            this.source = source;
            this.characterOffset = characterOffset;
        }
        public void Draw(SpriteBatch spriteBatch, Rectangle destinationRectangle, Color color) {
            float percentOffsetX = source.Width / destinationRectangle.Width;
            float percentOffsetY = source.Height / destinationRectangle.Height;
            Rectangle offsetDestinationRectangle = new Rectangle (
                (int)(characterOffset.X * percentOffsetX) + destinationRectangle.X,
                (int)(characterOffset.Y * percentOffsetY) + destinationRectangle.Y,
                destinationRectangle.Width,
                destinationRectangle.Height
                );

            spriteBatch.Draw(texture, offsetDestinationRectangle, color);
        }
    }
}
