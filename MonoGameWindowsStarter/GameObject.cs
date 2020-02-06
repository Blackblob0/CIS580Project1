using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter
{
    interface GameObject
    {
        void LoadContent();
        void Update(KeyboardState oldKeyboardState, KeyboardState newKeyboardState);
        void Draw(GameTime gameTime, SpriteBatch spriteBatch, bool showHitBoxes);
    }
}
