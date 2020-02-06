using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ArrayList gameObjects = new ArrayList();

        KeyboardState oldKeyboardState;
        KeyboardState newKeyboardState;
        bool showHitBoxes = false;
        public int hitBoxTransparency;
        Player player;

        public Texture2D pixel;

        public float gravity;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            player = new Player(this, 500, 500, 125, 250);
            gameObjects.Add(player);
            gameObjects.Add(new Wall(this, 0, 1000, 1920, 100));
            gameObjects.Add(new Coin(this, 800, 600));
            gameObjects.Add(new Coin(this, 950, 800));

            foreach (GameObject obj in gameObjects)
                if (obj is Wall || obj is Coin)
                    player.interactibleObjects.Add(obj);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            oldKeyboardState = Keyboard.GetState();
            gravity = 1f;

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixel = Content.Load<Texture2D>("Pixel");

            foreach(GameObject obj in gameObjects)
                obj.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            oldKeyboardState = newKeyboardState;
            newKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || newKeyboardState.IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
            foreach (GameObject obj in gameObjects)
                obj.Update(oldKeyboardState, newKeyboardState);

            if (newKeyboardState.IsKeyDown(Keys.X) && oldKeyboardState.IsKeyUp(Keys.X))
                showHitBoxes = !showHitBoxes;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            foreach (GameObject obj in gameObjects)
                obj.Draw(gameTime, spriteBatch, showHitBoxes);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
