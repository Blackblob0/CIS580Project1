using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace MonoGameWindowsStarter {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public ArrayList Walls = new ArrayList();
        public ArrayList Coins = new ArrayList();
        public ArrayList[] GameObjects;

        public KeyboardState oldKeyboardState;
        public KeyboardState newKeyboardState;
        bool showHitBoxes = false;
        public int hitBoxTransparency;
        public Player player;

        public Texture2D pixel;
        public Texture2D circle;
        public SpriteFont spriteFont;

        public float gravity;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            GameObjects = new ArrayList[] { Walls, Coins };

            new Player(this, 500, 500, 125, 250);
            new Wall(this, -1000, 1000, 3920, 100);
            new Coin(this, 800, 600);
            new Coin(this, 950, 800);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
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
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixel = Content.Load<Texture2D>("Sprites/Pixel");
            circle = Content.Load<Texture2D>("Sprites/Circle");
            spriteFont = Content.Load<SpriteFont>("Fonts/MangaTemple18");

            player.LoadContent();

            GameObject.GameObjectIterator(GameObjects, (obj) => {
                obj.LoadContent();
            });

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            oldKeyboardState = newKeyboardState;
            newKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || newKeyboardState.IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);

            player.Update();
            GameObject.GameObjectIterator(GameObjects, (obj) => {
                obj.Update();
            });


            if (newKeyboardState.IsKeyDown(Keys.X) && oldKeyboardState.IsKeyUp(Keys.X))
                showHitBoxes = !showHitBoxes;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            player.Draw(gameTime, spriteBatch, showHitBoxes);
            GameObject.GameObjectIterator(GameObjects, (obj) => {
                obj.Draw(gameTime, spriteBatch, showHitBoxes);
            });
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
