using System;
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

        //public ArrayList Levels = new ArrayList();

        public KeyboardState oldKeyboardState;
        public KeyboardState newKeyboardState;
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

            player = new Player(this, 0, 0);
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

            // Set up Parallax
            var backgroundTexture = Content.Load<Texture2D>("Parallax/Background");
            var backgroundSprite = new StaticSprite(backgroundTexture);
            var backgroundLayer = new ParallaxLayer(this);
            backgroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.1f);
            backgroundLayer.Sprites.Add(backgroundSprite);
            backgroundLayer.DrawOrder = 0;

            var midgroundTextures = new Texture2D[]
            {
                Content.Load<Texture2D>("Parallax/Midground1"),
                Content.Load<Texture2D>("Parallax/Midground2")
            };
            var midgroundSprites = new StaticSprite[]
            {
                new StaticSprite(midgroundTextures[0]),
                new StaticSprite(midgroundTextures[1], new Vector2(3500, 0))
            };
            var midgroundLayer = new ParallaxLayer(this);
            midgroundLayer.ScrollController = new PlayerTrackingScrollController(player, 0.4f);
            midgroundLayer.Sprites.AddRange(midgroundSprites);
            midgroundLayer.DrawOrder = 1;

            var gameLayer = new ParallaxLayer(this);
            gameLayer.ScrollController = new PlayerTrackingScrollController(player, 1.0f);
            gameLayer.Sprites.Add(player);
            gameLayer.DrawOrder = 2;


            Components.Add(backgroundLayer);
            Components.Add(midgroundLayer);
            Components.Add(gameLayer);

            //Levels.Add(Content.Load<Level>("Levels/Level1"));
            //Levels.Add(Content.Load<Level>("Levels/Level2"));
            //Levels.Add(Content.Load<Level>("Levels/Level3"));
            //foreach (Level level in Levels) level.SetGame(this);
            //((Level)Levels[0]).LoadLevel();

            Level lv = Content.Load<Level>("Levels/Level1");
            lv.SetGame(this);
            lv.LoadLevel();

            player.LoadContent();

            GameObject.GameObjectIterator(GameObjects, (obj) => {
                gameLayer.Sprites.Add(obj);
                obj.LoadContent();
            });
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

            //if (newKeyboardState.IsKeyDown(Keys.NumPad1) && oldKeyboardState.IsKeyUp(Keys.NumPad1)) ((Level)Levels[0]).LoadLevel();
            //if (newKeyboardState.IsKeyDown(Keys.NumPad2) && oldKeyboardState.IsKeyUp(Keys.NumPad2)) ((Level)Levels[1]).LoadLevel();
            //if (newKeyboardState.IsKeyDown(Keys.NumPad3) && oldKeyboardState.IsKeyUp(Keys.NumPad3)) ((Level)Levels[2]).LoadLevel();

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
